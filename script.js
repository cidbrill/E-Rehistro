window.addEventListener('load', function () {
    document.getElementById('signin-container').style.display = 'none';
    document.getElementById('nav-bar').style.display = 'none';
});

function toggleAuthenticationSlider() {
    const authenticationSlider = document.getElementById('authentication-slider');
    const signUpContainer = document.getElementById('signup-container');
    const signInContainer = document.getElementById('signin-container');

    if (this.id == 'sign-in') {
        authenticationSlider.style.left = `calc(100% - ${authenticationSlider.offsetWidth}px)`;
        authenticationSlider.style.boxShadow = '-5px 0 15px rgba(0, 0, 0, 0.4)';
        signUpContainer.style.left = `calc(50% - ${signUpContainer.offsetWidth / 2}px)`;
        setTimeout(() => {
            signInContainer.style.zIndex = '2';
            signUpContainer.style.zIndex = '1';
        }, 300);
            signInContainer.style.display = 'flex';
        setTimeout(() => {
            signInContainer.style.left = '0px';
        }, 50);
        setTimeout(() => {
            signUpContainer.style.display = 'none';
        }, 300);
    } else {
        authenticationSlider.style.left = '0px';
        authenticationSlider.style.boxShadow = '5px 0 15px rgba(0, 0, 0, 0.4)';
        signInContainer.style.left = `calc(50% - ${signInContainer.offsetWidth / 2}px)`;
        setTimeout(() => {
            signInContainer.style.zIndex = '1';
            signUpContainer.style.zIndex = '2';
        }, 300);
        signUpContainer.style.display = 'flex';
        setTimeout(() => {
            signUpContainer.style.left = 'calc(100% - 40vw)';
        }, 50);
        setTimeout(() => {
            signInContainer.style.display = 'none';
        }, 300);
    }
}

const switchToSignIn = document.getElementById('sign-in');
const switchToSignUp = document.getElementById('sign-up');

switchToSignIn.addEventListener('click', toggleAuthenticationSlider);
switchToSignUp.addEventListener('click', toggleAuthenticationSlider);

function clickFileUpload() {
    document.getElementById('fileUploadControl').click();
}
