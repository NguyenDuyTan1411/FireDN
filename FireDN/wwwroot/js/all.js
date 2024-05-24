var chart;
var labels = [];
var temperatureData = [];
var humidityData = [];
var maxDataPoints = 30;
var autoUpdate = false; // Biến cờ kiểm soát cập nhật tự động

async function sendCommand(command) {
    try {
        const response = await fetch('/api/control', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: `command=${command}`
        });

        if (response.ok) {
            updateStatus();
        } else {
            alert('Failed to send command');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Error sending command');
    }
}

async function updateStatus() {
    try {
        const response = await fetch('/api/control');
        if (response.ok) {
            const status = await response.text();
            document.getElementById('status').innerText = `Current Status: ${status}`;
        } else {
            document.getElementById('status').innerText = 'Failed to fetch status';
        }
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('status').innerText = 'Error fetching status';
    }
}

async function updateData() {
    try {
        const response = await fetch('/api/data');
        if (response.ok) {
            const data = await response.json();
            document.getElementById('temperature').innerText = `Temperature: ${data.temperature}°C`;
            document.getElementById('humidity').innerText = `Humidity: ${data.humidity}%`;

            // Thêm dữ liệu mới vào cuối mảng và loại bỏ dữ liệu cũ ở đầu mảng nếu đạt giới hạn
            temperatureData.push(data.temperature);
            humidityData.push(data.humidity);
            if (temperatureData.length > maxDataPoints) {
                temperatureData.shift();
                humidityData.shift();
                labels.shift();
            }

            // Tạo nhãn mới cho trục x
            const now = new Date();
            const timeString = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
            labels.push(timeString);

            return { temperatureData, humidityData };
        } else {
            console.error('Failed to fetch Arduino data');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

function drawLineChart(temperatureData, humidityData) {
    var ctx = document.getElementById('lineChart').getContext('2d');
    chart = new Chart(ctx, {
