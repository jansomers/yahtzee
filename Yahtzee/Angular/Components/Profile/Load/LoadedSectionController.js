function LoadedSectionController(gameService, chatService, $rootScope, $scope) {
    var ctrl = this;

    ctrl.myTurn = true;
    ctrl.currentTurn = 0;
    ctrl.newMessage = null;

    ctrl.getFullUser = function () {
        return $rootScope.fullUser;
    };
    ctrl.getLoadedGame = function () {
        return $rootScope.loadedGame;
    };

    ctrl.resetMessage = function() {
        ctrl.newMessage = null;
    }
    ctrl.sendMessage = function () {
        console.log("User tries to send a message");
        if (ctrl.newMessage !== "" && (ctrl.getLoadedGame().Status === 3 || ctrl.getLoadedGame().Status === 4)) {
            if (ctrl.getFullUser().screenName == null) {
                console.log("Alerting the user to choose in a nick name");
                alert("Did you know you can choose a nick name in the user section? This will be displayed in chats and games");
            }
            console.log("User has rights to senda message");
            chatService.postMessage(ctrl.getLoadedGame(), ctrl.getFullUser(), ctrl.newMessage)
                .then(function (data) {
                    ctrl.resetMessage();
                    console.log("Message succesfully posted, reloading loaded game for both players" + data.data);
                    $rootScope.loadedGame = data.data;
                    $rootScope.hub.invoke("GameChange", ctrl.getLoadedGame().Id);
                });
        }
    };

    ctrl.isPlayerA = function () {
        return (ctrl.getLoadedGame().PlayerA.UserName === ctrl.getFullUser().userName);
    };
    ctrl.isPlaying = function () {
        var game = ctrl.getLoadedGame();
        /* console.log("Game status: " + game.Status + "/// Is 3? " + (game.Status === 3));
         console.log("Game Player A : " + ctrl.isPlayerA());
         console.log("Game Player B : " + !ctrl.isPlayerA());*/

        if (typeof game != 'undefined' && game !== null) {
            if (game.Status === 3 && ctrl.isPlayerA()) {
                return true;
            }
            if (game.Status === 4 && !ctrl.isPlayerA()) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }

    };
    /**
     * Gets the text to be displayed on the dice button 
     */
    ctrl.getDiceText = function () {
        if (ctrl.isPlaying()) {
            if (ctrl.currentTurn === 3) {
                return "No more rolls";
            } else {
                return "Roll the dice!";
            }
        } else {
            if (ctrl.hasGameEnded()) {
                return "Game has ended";
            } else {
                return "Not your turn";
            }
        }
    };
    /**
     * Javascript object where I store the classes for each possible dice result
     */
    ctrl.diceClass = {
        one: 'one',
        two: 'two',
        three: 'three',
        four: 'four',
        five: 'five',
        six: 'six'
    };
    /**
     * Javascript object where I define the dice and control things as results (result), display (cssClass) and if the user kept them (selected)
     */
    ctrl.dice = {
        die1: {
            cssClass: null,
            result: null,
            selected: false
        },
        die2: {
            cssClass: null,
            result: null,
            selected: false
        },
        die3: {
            cssClass: null,
            result: null,
            selected: false
        },
        die4: {
            cssClass: null,
            result: null,
            selected: false
        },
        die5: {
            cssClass: null,
            result: null,
            selected: false
        }
    };

    /**
     * Reset the dice
     */
    ctrl.resetDice = function () {
        for (var die in ctrl.dice) {
            if (ctrl.dice.hasOwnProperty(die)) {
                console.log(die);
                ctrl.dice[die].cssClass = null;
                ctrl.dice[die].result = null;
                ctrl.dice[die].selected = false;
            }
        }
        console.log(ctrl.dice);
        $("#dice-div").children().removeClass().addClass("die");

    };

    /**
     * Selects a die (selected dice won't be rolled again the next time the user rolls the dice)
     * @param {} $event (.currentTarget to get the die clicked on the page)
     * @param {} index  (which die of the dice is clicked)
     */
    ctrl.toggleDie = function ($event, index) {
        if (ctrl.currentTurn > 0) {
            var dieToToggle = 'die' + index;
            console.log("User toggles die " + index);
            $($event.currentTarget).toggleClass('die-selected');
            $($event.currentTarget).toggleClass('pull-left');
            ctrl.dice[dieToToggle].selected = !ctrl.dice[dieToToggle].selected;
        }
    };
    ctrl.rollDie = function (rolledDie) {
        var outcome = Math.floor(Math.random() * 6) + 1;
        ctrl.dice[rolledDie].result = outcome;
        /**
         *  Showing the user the right number of eyes on the die
        */
        switch (outcome) {
            case 1:
                ctrl.dice[rolledDie].cssClass = 'one';
                break;
            case 2:
                ctrl.dice[rolledDie].cssClass = 'two';
                break;
            case 3:
                ctrl.dice[rolledDie].cssClass = 'three';
                break;
            case 4:
                ctrl.dice[rolledDie].cssClass = 'four';
                break;
            case 5:
                ctrl.dice[rolledDie].cssClass = 'five';
                break;
            case 6:
                ctrl.dice[rolledDie].cssClass = 'six';
                break;
        }
    };
    ctrl.rollDice = function () {
        if (ctrl.isPlaying() && ctrl.currentTurn < 3) {
            for (var i = 1; i < 6; i++) {
                var dieToRoll = 'die' + i;
                if (ctrl.dice[dieToRoll].selected === false) {
                    ctrl.rollDie(dieToRoll);
                };
            }
            ctrl.currentTurn++;
        }
    };
    ctrl.resetTurn = function() {
        ctrl.currentTurn = 0;
    };
    ctrl.getDiceResults = function () {
        console.log("Getting dice results");
        return [ctrl.dice.die1.result, ctrl.dice.die2.result, ctrl.dice.die3.result, ctrl.dice.die4.result, ctrl.dice.die5.result];
    };
    ctrl.countInArray = function (array, key) {
        var result = 0;
        for (var i = 0; i < array.length; i++) {
            if (array[i] === key) {
                result += key;
            }
        }
        return result;
    };
    ctrl.getTotalUpperBeforeBonus = function (key) {
        var game = ctrl.getLoadedGame();
        if (game != null) {
            var gameResult;
            if (key === "A") {
                gameResult = game.GameResultA;
            } else {
                gameResult = game.GameResultB;
            }
            return (gameResult.Ones +
                gameResult.Twos +
                gameResult.Threes +
                gameResult.Fours +
                gameResult.Fives +
                gameResult.Sixes);
        } else {
            return 0;
        }
    };
    ctrl.getBonus = function (key) {
        if (ctrl.getTotalUpperBeforeBonus(key) > 63) {
            return 35;
        } else {
            return 0;
        }
    };
    ctrl.getTotalUpperAfterBonus = function(key) {
        return ctrl.getTotalUpperBeforeBonus(key) + ctrl.getBonus(key);
    };
    ctrl.getTotalLower = function (key) {
        var game = ctrl.getLoadedGame();
        if (game != null) {
            var gameResult;
            if (key === "A") {
                gameResult = game.GameResultA;
            } else {
                gameResult = game.GameResultB;
            }
            return (gameResult.Tok +
                gameResult.Fok +
                gameResult.Fh +
                gameResult.SmStr +
                gameResult.LaStr +
                gameResult.Yahtzee +
                gameResult.Chance);
        } else {
            return 0;
        }
    };

    ctrl.hasGameEnded = function () {
        if (ctrl.getLoadedGame() != null) {
            var gameResultA = ctrl.getLoadedGame().GameResultA;
            var gameResultB = ctrl.getLoadedGame().GameResultB;
            var ended = true;
            for (var propA in gameResultA) {
                if (gameResultA.hasOwnProperty(propA)) {
                    if (gameResultA[propA] === 0) {
                        ended = false;
                    }
                }
            }
            for (var propB in gameResultB) {
                if (gameResultB.hasOwnProperty(propB)) {
                    if (gameResultB[propB] === 0) {
                        ended = false;
                    }
                }
            }
            return ended;
        } else {
            return false;
        }
    };
    /**
     * Get the screenName of the player that won the game
     * @returns {} 
     */
    ctrl.getWinner = function () {
        if (ctrl.getTotalLower('A') + ctrl.getTotalUpperAfterBonus('A') >
            ctrl.getTotalLower('B') + ctrl.getTotalUpperAfterBonus('B')) {
            return ctrl.getLoadedGame().PlayerA.ScreenName;
        } else {
            return ctrl.getLoadedGame().PlayerB.ScreenName;
        }
    };
    
    ctrl.endGame = function () {
        console.log("Ending the game: " + ctrl.getLoadedGame().GameName);
        var resultA = ctrl.getTotalUpperAfterBonus('A') + ctrl.getTotalLower('A');
        var resultB = ctrl.getTotalUpperAfterBonus('B') + ctrl.getTotalLower('B');
        var winnerId = resultA > resultB ? ctrl.getLoadedGame().PlayerAId : ctrl.getLoadedGame().PlayerBId;
        gameService.endGame(ctrl.getLoadedGame().Id, resultA, resultB, winnerId)
            .then(function (data) {
                console.log("Game successfully ended: reloading loaded game and updating usergames " + ctrl.getLoadedGame().GaneName);
                $rootScope.loadedGame = data.data;
                $rootScope.hub.invoke("GameChange", ctrl.getLoadedGame().Id);
            });
    };
    ctrl.registerUpperScore = function (key, result) {
        console.log("User registered upperscore");
        gameService.registerUpperScore(key, result, ctrl.isPlayerA(), ctrl.getLoadedGame()).then(function (data) {
            console.log("Successfully submitted upper score. Resetting turn and dice");
            ctrl.resetTurn();
            ctrl.resetDice();
            $rootScope.loadedGame = data.data;
            
            if (ctrl.hasGameEnded()) {
                console.log("Game has ended, ending the game");
                ctrl.endGame();

            } else {
                console.log("Game has not ended yet");
                $rootScope.updateUserGames();
                $rootScope.hub.invoke("GameChange", ctrl.getLoadedGame().Id);
            }
        });
    };
    ctrl.registerLowerScore = function (key, result) {
        console.log("User registered lowerscore");
        gameService.registerLowerScore(key, result, ctrl.isPlayerA(), ctrl.getLoadedGame())
            .then(function (data) {
                console.log("Succesfully submitted lower score.Resetting turn and dice");
                ctrl.resetTurn();
                ctrl.resetDice();
                $rootScope.loadedGame = data.data;
                
                if (ctrl.hasGameEnded()) {
                    ctrl.endGame();
                } else {
                    $rootScope.updateUserGames();
                    $rootScope.hub.invoke("GameChange", ctrl.getLoadedGame().Id);
                }
            });
    };
    /**
     * Method that validates the upper result option that the user selected.
     * @param {} key 
     * @returns {} true if the score can be registered.
     */
    ctrl.isValidUpperOption = function(key) {
        var valid;
        switch (key) {
        case 1:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Ones > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Ones > 0 ? false : true);
            break;
        case 2:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Twos > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Twos > 0 ? false : true);
            break;
        case 3:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Threes > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Threes > 0 ? false : true);
            break;
        case 4:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Fours > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Fours > 0 ? false : true);
            break;
        case 5:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Fives > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Fives > 0 ? false : true);
            break;
        case 6:
            valid = ctrl.isPlayerA()
                ? (ctrl.getLoadedGame().GameResultA.Sixes > 0 ? false : true)
                : (ctrl.getLoadedGame().GameResultB.Sixes > 0 ? false : true);
            break;
        default:
            valid = false;
        }
        return valid;
    };
    /**
    * Method that validates the lower result option that the user selected.
    * @param {} key 
    * @returns {} true if the score can be registered.
    */
    ctrl.isValidLowerOption = function (key) {
        var valid = false;
        switch (key) {
            case 1:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.Tok > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.Tok > 0 ? false : true);
                break;
            case 2:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.Fok > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.Fok > 0 ? false : true);
                break;
            case 3:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.Fh > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.Fh > 0 ? false : true);
                break;
            case 4:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.SmStr > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.SmStr > 0 ? false : true);
                break;
            case 5:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.LaStr > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.LaStr > 0 ? false : true);
                break;
            case 6:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.Yahtzee > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.Yahtzee > 0 ? false : true);
                break;
            case 7:
                valid = ctrl.isPlayerA()
                    ? (ctrl.getLoadedGame().GameResultA.Chance > 0 ? false : true)
                    : (ctrl.getLoadedGame().GameResultB.Chance > 0 ? false : true);
                break;
            default:
                valid = false;
        }
        return valid;
    };
    /**
     * Method that check if an array contains a number x-times
     * @param {} diceSet the array
     * @param {} times how many times it should be in the array
     * @returns {}  true if the array contains at least x-times the same number
     */
    ctrl.containsAtLeast = function (diceSet, times) {
        var array = diceSet.sort();
        var counter = 1;
        var current = 0;
        var containsTimes = false;

        for (var i = 0; i < array.length; i++) {

            if (i === 0) {
                current = array[i];

            } else {
                if (current === array[i]) {
                    counter++;
                } else {
                    counter = 1;
                    current = array[i];
                }
            }
            if (counter >= times) {
                containsTimes = true;
            }
        }

        return containsTimes;
    };
    /**
     * This method may seem a little bit weird at first, but what I do is I put the amount of times a result occurs in another array.
     * A full house is two times a number and three times a number (eg. 1 1 and 3 3 3). My result array would look as following:
     * [2,0,3,0,0,0]. I then check to see if there is a number that appears two and AND a number that appears three times. 
     * @param {} diceSet 
     * @returns {} returns true if the diceset is a full house.
     */
    ctrl.isFullHouse = function (diceSet) {
        var result = [0, 0, 0, 0, 0, 0];
        console.log(diceSet);
        for (var i = 0; i < diceSet.length; i++) {
            result[diceSet[i] - 1]++;
        }

        return result.includes(2) && result.includes(3);


    };
    /**
     * This function returns the different values of an array without duplicates.
     * @param {} diceSet 
     * @returns {} a unique array.
     */
    ctrl.getUniqueValues = function (diceSet) {
        var arr = [];
        for (var i = 0; i < diceSet.length; i++) {
            if (!arr.includes(diceSet[i])) {
                arr.push(diceSet[i]);
            }
        }
        return arr;
    };

    /**
     * This function takes an array of numbers and a type (either 1: small straight or 2 : large straight). The first thing I do
     * is to remove the duplicates by callign the getUniqueValues() method. I then sort the array to line the numbers up. I keep track of two variabeles.
     * Counter = How many numbers in a row and Previous = The value of the previous number. As I start the counter gets set to 1. 
     * Assuming the first number is 1 var previous = 1 at the end of the iteration. Assuming the next number is 2 to counter goes up ->
     * because 2 != 1 and 2-1 = 1 (meaning they're adjacent). If the counter is at least 4 smallStraight becomes true and if it's 5 largestraight becomes true.
     * @param {} diceSet 
     * @param {} type 
     * @returns {} if the type is 1 it returns smallStraight, else toReturn = largeStraight 
     */
    ctrl.isStraight = function (diceSet, type) {
        var uniques = ctrl.getUniqueValues(diceSet);
        var sortedUniques = uniques.sort();
        console.log(sortedUniques);
        var counter = 0;
        var previous = 0;
        var smallStraight = false;
        var largeStraight = false;
        var toReturn = false;
        for (var i = 0; i < sortedUniques.length; i++) {
            if (sortedUniques[i] !== previous && (sortedUniques[i] - previous === 1)) {
                counter++;
            } else {
                counter = 1;
            }
            if (counter === 4) {
                smallStraight = true;
            }
            if (counter === 5) {
                largeStraight = true;
            }
            previous = sortedUniques[i];
        }
        if (type === 1) {
            toReturn = smallStraight;
        }
        if (type === 2) {
            toReturn = largeStraight;
        }
        return toReturn;
    };
    
    ctrl.calculateLowerResult = function (diceSet, key) {
        var result = 0;
        switch (key) {
            case 1:
                result = ctrl.containsAtLeast(diceSet, 3) ? diceSet.reduce((pv, cv) => pv + cv, 0) : 0;
                break;
            case 2:
                result = ctrl.containsAtLeast(diceSet, 4) ? diceSet.reduce((pv, cv) => pv + cv, 0) : 0;
                break;
            case 3:
                result = ctrl.isFullHouse(diceSet) ? 25 : 0;
                break;
            case 4:
                result = ctrl.isStraight(diceSet, 1) ? 30 : 0;
                break;
            case 5:
                result = ctrl.isStraight(diceSet, 2) ? 40 : 0;
                break;
            case 6:
                result = ctrl.containsAtLeast(diceSet, 5) ? 50 : 0;
                break;
            case 7:
                result = diceSet.reduce((pv, cv) => pv + cv, 0);
                break;


            default:
                result = 0;
        }

        return result;
    };



    ctrl.updateLowerScore = function (key) {
        console.log("User wants to select lower score");
        var valid = ctrl.isValidLowerOption(key);
        if (ctrl.isPlaying() && valid) {
            console.log("Option was valid. Updating Score for: " + key);
            var diceSet = ctrl.getDiceResults();
            if (diceSet[0] > 0) {
                var result = ctrl.calculateLowerResult(diceSet, key);
                if (result === 0) {
                    //Small workaround to give the player at least 1.
                    alert("Damn, the dice just didn't help you this time!");
                    result++;}
                ctrl.registerLowerScore(key, result);
            } else {
                alert("You haven't even rolled yet!?");
            }
        } else {
            if (!ctrl.isPlaying() && !ctrl.hasGameEnded()) {
                alert("It's not your turn");
            }
            if (ctrl.hasGameEnded()) {
                alert("The game has ended");
            }
            if (!valid && ctrl.isPlaying()) {
                alert("You already registered that score!");
            }
        }
    };

    ctrl.updateUpperScore = function (key) {
        console.log("User wants to select upper score");
        var valid = ctrl.isValidUpperOption(key);
        if (ctrl.isPlaying() && valid) {
            console.log("Option was valid. Updating Score for: " + key);
            var diceSet = ctrl.getDiceResults();
            if (diceSet[0] > 0) {
                var result = ctrl.countInArray(diceSet, key);
                if (result === 0) {
                    //Small workaround to give the player at least 1.
                    alert("Poor you! Here, get a point .. for effort?");
                    result++;
                }
                ctrl.registerUpperScore(key, result);
            } else {
                alert("You haven't even rolled yet!?");
            }

        } else {
            if (!ctrl.isPlaying() && !ctrl.hasGameEnded()) {
                alert("It's not your turn");
            }
            if (ctrl.hasGameEnded()) {
                alert("The game has ended");
            }
            if (!valid && ctrl.isPlaying()) {
                alert("You already registered that score!");
            }
        }
    };
    /**
    * Listener to listen for gameChanges. If your loaded game has been updated, then the active game list gets refreshed and loadedGame gets set to the updated version.
    */
    var listener = $rootScope.$on("GameChange",
    function (ev, id) {
        console.log("Pushing game: " + id);
        if (ctrl.getLoadedGame().Id === id) {
            var clonedGame = $rootScope.loadedGame;
            var winner = ctrl.getWinner();
            gameService.active()
                .success(function (data) {
                    console.log("Active games refreshed, Updating loadedGame");
                    $rootScope.loadedGame = data.filter(function (obj) {
                        console.log("Refreshed loaded game");
                        return obj.Id == id;
                    })[0];
                    if ($rootScope.loadedGame == null) {
                        alert(clonedGame.GameName + " has ended!\n" + winner + " wins!");
                        $rootScope.updateFullUser();
                    }
                });
          

        }
    });

    $scope.$on('$destroy', function () {
        listener();
    });
};

angular.module('Yahtzee')
    .component('loadedSection',
    {
        controller: LoadedSectionController,
        templateUrl: 'Angular/Components/Profile/Load/loaded-section.html'
    });