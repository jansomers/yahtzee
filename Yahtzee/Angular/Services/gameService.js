angular.module("Yahtzee").service('gameService', function ($http) {

    this.get = function(id) {
        console.log("Forwarding Get Request to GameController");
        var resp = $http({
            url: "/api/Game/GetGame",
            method: "GET",
            params: { id: id }
        });
        return resp;
    };

    this.makeGame = function (email, name) {
        console.log("Forwarding MakeGame Request to GameController");
        console.log("with params:\ninvited user :" + email + " name: " + name);
        var resp = $http({
            url: "/api/Game/MakeGame",
            method: "POST",
            params: { emailB: email, gameName: name }
        });
        return resp;
    };

    this.acceptGame = function(gameToAccept) {
        console.log("Forwarding AcceptGame Request to GameController");
        console.log("with param:\ngame: " + gameToAccept.Id);
        var resp = $http({
            url: "/api/Game/AcceptGame",
            method: "PUT",
            params: { game: gameToAccept.Id }
        });
        return resp;

    };

    this.removeGame = function(gameToRemove) {
        console.log("Forwarding RemoveGame Request to GameController");
        console.log("with param:\ngame: " + gameToRemove.Id);
        console.log(gameToRemove.Id);
        var resp = $http({
            url: "/api/Game/RemoveGame",
            method: "DELETE",
            params: { game: gameToRemove.Id }
        });
        return resp;
    };

    this.invitations = function () {
        console.log("Forwarding Invitations request to GameController");
        var resp = $http({
            url: "/api/Game/Invitations",
            method: "GET"
        });
        return resp;
    };

    this.active = function () {
        console.log("Forwarding Active games request to GameController");
        var resp = $http({
            url: "/api/Game/ActiveGames",
            method: "GET"
        });
        return resp;
    };

    this.history = function () {
        console.log("Forwarding History request to GameController");
        var resp = $http({
            url: "/api/Game/History",
            method: "GET"
        });
        return resp;
    };

    this.registerUpperScore = function(key, result, isPlayerA, gameToUpdate) {
        console.log("Forwarding upper section update to GameController");
        var resp = $http({
            url: "/api/Game/PutUpperResult",
            method: "PUT",
            params: { key: key, score: result, playerA: isPlayerA, gameId: gameToUpdate.Id }
        });
        return resp;
    };

    this.registerLowerScore = function(key, result, isPlayerA, gameToUpdate) {
        console.log("Forwarding lower section update to GameController");
        var resp = $http({
            url: "/api/Game/PutLowerResult",
            method: "PUT",
            params: { key: key, score: result, playerA: isPlayerA, gameId: gameToUpdate.Id }
        });
        return resp;
    };

    this.endGame = function(gameId, resultA, resultB, winnerId) {
        console.log("Forwarding end game request to GameController");
        var resp = $http({
            url: "/api/Game/EndGame",
            method: "PUT",
            params: { gameId: gameId, scoreA: resultA, scoreB: resultB, winnerId: winnerId }
        });
        return resp;
    }


});