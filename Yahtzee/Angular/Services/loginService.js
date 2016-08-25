angular.module("Yahtzee").service('loginService', function ($http,$rootScope) {

    this.register = function (userInfo) {
        console.log("Forwarding Register Request to AccountController");
        var resp = $http.post("/api/Account/Register",userInfo);
        return resp;
    };

    this.login = function (userlogin) {
        console.log(" Forwarding Token (Login) Request to AccountController");
        var resp = $http({
            url: "/Token",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return resp;
    };

    this.logout = function () {
        console.log("Forwarding Logout Request to AccountController");
        $http.post("/api/Account/Logout").then(function () {
            console.log("Logout succesfull, clearing data! ");
            $rootScope.user = null;
            sessionStorage.setItem('userName', '');
            sessionStorage.setItem('accessToken', '');
            sessionStorage.setItem('refreshToken', '');
            location.reload();
        });
    };


});