var Yahtzee = angular.module('Yahtzee', []);

/**
 * Configuring to pass token with every http request. 
 */
Yahtzee.config([
    '$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(['$q', '$location', function ($q) {
            var accesstoken = sessionStorage.getItem('accessToken');
            return {
                'request': function (config) {
                    config.headers = config.headers || {};
                    if (accesstoken) {
                        config.headers.Authorization = 'Bearer ' + accesstoken;
                    }
                    return config;
                },
                'responseError': function (response) {
                    if (response.status === 401 || response.status === 403) {
                        
                    }
                    return $q.reject(response);
                }
            };
        }]);
    }
]);

function initializeHub($rootScope) {
    var hub = $.connection.appHub;
    $.connection.hub.url = "/signalr";

    hub.client.gameChange = function(id) {
        $rootScope.$emit("GameChange", id);
    };

    $.connection.hub.start().fail(function (error) {
        console.log(error);
    }).done(function () { console.log("Hub started connection!")});
    $rootScope.hub = hub;
}

Yahtzee.run(['$rootScope', 'userService', 'gameService', function ($rootScope, userService, gameService) {
    $rootScope.user = null;
    $rootScope.stats = {
        wins: null,
        best: null,
        average: null
    };
    $rootScope.fullUser = null;
    $rootScope.games = {
        invitations: null,
        activeGames: null,
        history: null
    };
    $rootScope.loadedGame = null;
    initializeHub($rootScope);
    userService.get()
           .then(function (data) {
               $rootScope.user = data.data;
           });

    $rootScope.isLoggedin = function () {
        var loggedIn = $rootScope.user !== null;
        return loggedIn;
    };

    $rootScope.updateUserGames = function() {
        console.log("Updating the invitations");
        gameService.invitations()
            .then(function(data) {
                console.log('Retrieved invitations');
                $rootScope.games.invitations = data.data;
            });
        gameService.active()
            .then(function(data) {
                console.log('Retrieved active games');
                $rootScope.games.activeGames = data.data;
            });
        gameService.history()
            .then(function(data) {
                console.log('Retrieved history of games');
                $rootScope.games.history = data.data;
            });
    };


    $rootScope.updateUser = function() {
        console.log('Updating the user');
        userService.details()
            .then(function (data) {
                $rootScope.fullUser = {
                    userName: data.data.UserName,
                    screenName: data.data.ScreenName,
                    avatar: data.data.Avatar,
                    friends: data.data.Friends};
                console.log($rootScope.fullUser);
                console.log(data.data.Friends);
            });
    };

    $rootScope.updateStats = function () {
        if ($rootScope.user != null) {
            console.log("Updating wins");
            userService.wonGames()
                .then(function(data) {
                    $rootScope.stats.wins = data.data;
                });
            userService.bestScore()
                .then(function(data) {
                    $rootScope.stats.best = data.data;
                });
            userService.averageScore()
                .then(function(data) {
                    $rootScope.stats.average = data.data;
                });
        }
    };

    $rootScope.updateFullUser = function () {
        console.log("Updating the user");
        
        $rootScope.updateUser();
        $rootScope.updateUserGames();
        $rootScope.updateStats();


    };

    window.onload = function ()
    {
        
        $rootScope.updateFullUser();
    }


}]);