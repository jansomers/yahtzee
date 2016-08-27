function LoginController(loginService, userService, $rootScope, $window) {
    var ctrl = this;

    ctrl.Reset = function () {
        ctrl.loginModel = {
            username: '',
            password: ''
        };
    };

    ctrl.Login = function () {
        console.log("User clicked login button. Assigning grant type and extracting username and password");
        var userLogin = {
            grant_type: 'password',
            username: ctrl.loginModel.username,
            password: ctrl.loginModel.password
        };
  
        
        var promiselogin = loginService.login(userLogin);

        promiselogin.then(function (resp) {
            console.log("Login Successful, storing username and token information");
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            console.log("Authentication information stored");
            console.log("Login Success");
            $window.location.reload();

        }, function (err) {
            console.log("Login Error");
            console.log(err);
            alert("You entered the wrong details, try again");
            ctrl.Reset();
        });
    };

   

    ctrl.Reset();
}

angular.module('Yahtzee')
    .component('login',
    {
        controller: LoginController,
        templateUrl: '/Angular/Components/Login/login.html'
    });


