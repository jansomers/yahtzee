function GamesSectionController(userService,gameService, $rootScope) {
    var ctrl = this;

    ctrl.getInvitationsForUser = function() {
        return $rootScope.games.invitations;
    };
    ctrl.getActiveGames = function() {
        return $rootScope.games.activeGames;
    };
    ctrl.getHistory = function() {
        return $rootScope.games.history;
    };
    ctrl.acceptGame = function(game) {
        console.log("User clicked Accept Game Button for: " + game.GameName);
        gameService.acceptGame(game)
            .then(function() {
                console.log("Game Successfully accepted: Updating the user");
                $rootScope.updateFullUser();
            });
    };
    ctrl.loadGame = function(game) {
        console.log("User wants to load game: " + game.GameName);
        $rootScope.loadedGame = game;

        if (game === $rootScope.loadedGame) {
            console.log("Game was loaded successfully");
        } else {
            console.log("Game was loaded unsuccessfully");
        }
    };
    ctrl.deleteGame = function(game) {
        console.log("User wants to delete game: " + game.GameName);
        gameService.removeGame(game)
            .then(function() {
                console.log("Game Successfully removed, resetting loaded game in rootscope and updating user");
                $rootScope.loadedGame = null;
                $rootScope.updateFullUser();
            });
    };

    ctrl.updateUserGames = function() {
        var icon = "#refresh-games-button";
        $(icon).removeClass("mdi-refresh");
        $(icon).addClass("mdi-autorenew");
        $rootScope.updateUserGames();
        setTimeout(function () {
            $(icon).removeClass("mdi-autorenew");
            $(icon).addClass("mdi-refresh");
        }, 300);
    }
}



angular.module('Yahtzee')
    .component('gamesSection',
        {
            controller: GamesSectionController,
            templateUrl: 'Angular/Components/Profile/Games/games-section.html',
            bindings: {
                loadedGame: '='
            }
        }
    );