angular.module("Yahtzee").service('userService', function ($http) {
    this.get = function () {
        console.log("Forwarding UserInfo Request to AccountController");
        var response = $http({
            url: "/api/Account/UserInfo",
            method: "GET"
        });
        return response;
    };

    this.details = function () {
        console.log("Forwarding Own details Request to UserController");
        var response = $http({
            url: "/api/User/DetailsByEmail/",
            method: "GET",
            params: { email: sessionStorage.getItem('userName') }

        });
        return response;

    };

    this.otherDetails = function (userName) {
        console.log("Forwarding Other details request to UserController");
        var response = $http({
            url: "/api/User/DetailsByEmail/",
            method: "GET",
            params: { email: userName }
        });
        return response;
    };

    this.updateScreenName = function (newScreenName) {
        console.log("Forwarding Update ScreenName request to UserController");
        var response = $http({
            url: "/api/User/UpdateScreenName/",
            method: "PUT",
            params: { screenName: newScreenName }

        });
        return response;

    };

    this.updateAvatar = function (newAvatar) {
        console.log('Forwarding Update Avatar request to UserController');
        var response = $http({
            url: "/api/User/UpdateAvatar/",
            method: "PUT",
            params: { avatar: newAvatar }
        });
        return response;
    };

    this.wonGames = function() {
        console.log('User Service making WonGames Request');
        var resp = $http({
            url: "/api/User/WonGames",
            method: "GET"
        });
        return resp;
    };

    this.bestScore = function() {
        console.log('User Service making BestScore Request');
        var resp = $http({
            url: "/api/User/BestScore",
            method: "GET"
        });
        return resp;
    };

    this.averageScore = function() {
        console.log('User Service making AvgScore request');
        var resp = $http({
            url: "/api/User/AvgScore",
            method: "GET"
        });
        return resp;
    };

});