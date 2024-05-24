#include <WiFi.h>
#include <HTTPClient.h>
#include <DHT.h>
#include <cstring> // Thêm thư viện cstring

#define DHTPIN 13     // Chân kết nối DHT11
#define DHTTYPE DHT11
//#define FIRE_SENSOR_PIN 14 
//#define SMOKE_SENSOR_PIN 10// Loại cảm biến DHT

const char* ssid = "Nhà trọ 2";
const char* password = "88888888";
const char* serverBaseAddress = "http://192.168.0.112:5000/"; // Địa chỉ URL cơ sở của API
const char* controlBaseAddress = "http://192.168.0.112:5000/"; // Địa chỉ URL để nhận tín hiệu


// Khai báo chuỗi serverAddress và controlAddress
char serverAddress[100]; // Đảm bảo đủ lớn để chứa địa chỉ URL
char controlAddress[100];

DHT dht(DHTPIN, DHTTYPE);

bool scanningEnabled = false;

void setup() {
  Serial.begin(115200);
  dht.begin();
  
  // Kết nối tới mạng Wi-Fi
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi..");
  }
  
  Serial.println("Connected to WiFi");
  
  // Nối địa chỉ URL cơ sở với địa chỉ cụ thể cho server và control
  strcpy(serverAddress, serverBaseAddress);
  strcat(serverAddress, "data");
  strcpy(controlAddress, serverBaseAddress);
  strcat(controlAddress, "control");
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    checkControlSignal(); // Kiểm tra tín hiệu từ ASP.NET

    float humidity = dht.readHumidity();
    float temperature = dht.readTemperature();

    if (isnan(humidity) || isnan(temperature)) {
      Serial.println("Failed to read from DHT sensor!");
      return;
    }

    if (scanningEnabled || temperature > 40) {
      sendData(temperature, humidity);
    }
  }

  delay(3000); // Chờ 3 giây trước khi gửi yêu cầu tiếp theo
}

void sendData(float temperature, float humidity) {
  HTTPClient http;
  String postData = "temperature=" + String(temperature) + "&humidity=" + String(humidity);
  Serial.print("Sending data: ");
  Serial.println(postData);

  http.begin(serverAddress);
  http.addHeader("Content-Type", "application/x-www-form-urlencoded");

  int httpCode = http.POST(postData);

  if (httpCode > 0) {
    Serial.printf("[HTTP] POST request sent, status code: %d\n", httpCode);
  } else {
    Serial.printf("[HTTP] POST request failed, error: %s\n", http.errorToString(httpCode).c_str());
  }

  http.end();
}

void checkControlSignal() {
  HTTPClient http;
  http.begin(controlAddress);
  int httpCode = http.GET();

  if (httpCode > 0) {
    String payload = http.getString();
    scanningEnabled = (payload == "start");
    Serial.printf("[HTTP] GET control signal, payload: %s\n", payload.c_str());
  } else {
    Serial.printf("[HTTP] GET control signal failed, error: %s\n", http.errorToString(httpCode).c_str());
  }

  http.end();
}
