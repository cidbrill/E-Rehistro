window.addEventListener('load', function () {
    document.getElementById('signup-container').style.display = 'none';
});

function toggleSlider() {
    const authenticationSlider = document.getElementById('authentication-slide');
    const signInContainer = document.getElementById('signin-container');
    const signUpContainer = document.getElementById('signup-container');

    if (this.id == 'sign-in') {
        authenticationSlider.style.right = '0vw';
        signInContainer.style.left = '0vw';
        signUpContainer.style.display = 'none';
    } else {
        authenticationSlider.style.left = '0vw';
        signInContainer.style.display = 'none';
        signUpContainer.style.right = '0vw';
    }
}

var signInButton = document.getElementById('sign-in');
var signUpButton = document.getElementById('sign-up');

signInButton.addEventListener('click', toggleSlider);
signUpButton.addEventListener('click', toggleSlider);