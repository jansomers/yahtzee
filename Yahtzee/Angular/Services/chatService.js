angular.module("Yahtzee").service('chatService',function($http) {

    this.postMessage = function (game, user, message) {
        console.log("Forwarding PostChat request to GameController: ");
        console.log("GameId: " + game.Id + " || Message : " + message);
        var gameId = game.Id;
        var screenName = user.screenName;
        var resp = $http({
            url: "/api/Game/PostChat",
            method: "POST",
            params: {gameId : gameId, name: screenName, message: message}
        });
        return resp;
    };


});