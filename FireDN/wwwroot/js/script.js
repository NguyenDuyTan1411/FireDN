document.getElementById("login-button").addEventListener("click", function (event) {
    event.preventDefault();
    document.querySelector('form').style.display = 'none';
    document.querySelector('.wrapper').classList.add('login-success');
    document.getElementById('welcome-message').innerText = 'Fire Alarm';
});
