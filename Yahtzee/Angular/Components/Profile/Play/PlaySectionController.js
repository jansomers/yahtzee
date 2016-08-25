function PlaySectionController(userService, gameService, $rootScope) {
    var ctrl = this;

    ctrl.newInvite = null;
    ctrl.gameName = null;
    ctrl.invitedUser = null;


    ctrl.getFullUser = function() {
        return $rootScope.fullUser;
    };

    ctrl.resetInvitedUser = function() {
        console.log('Resetting previously invited user');
        ctrl.invitedUser = null;
    };
    ctrl.resetGameName = function() {
        console.log('Resetting gamename');
        ctrl.newInvite = null;
        ctrl.gameName = null;
    };
    /**
     * Before we can make the game we need to make sure the invited player is not you or empty.
     * @param {} email 
     */
    ctrl.verifyEmailToInvite = function (email) {
        console.log('Verifying email');
        if (email != null && email !== ctrl.getFullUser().userName) {
            return true;
        } else {
            if (email == null) {
                console.log("Email is blank; Not inviting");

            }
            if (email === ctrl.getFullUser().userName) {
                console.log('Email is yours; Not invting');

            }
            return false;
        }
    };

    ctrl.makeGame = function() {
        var promiseGame = gameService.makeGame(ctrl.invitedUser.UserName, ctrl.gameName);

        promiseGame.then(function(resp) {
            console.log("Game Succesfully Made");
            console.log("Game object: " + resp.data);
            ctrl.resetInvitedUser();
            ctrl.resetGameName();
            $rootScope.updateFullUser();
        });
    };
    ctrl.selectFriend = function(email) {
        ctrl.newInvite = email;
    };
    ctrl.invitePlayer = function() {
        console.log('User clicked invite player');
        ctrl.resetInvitedUser();
        var invite = ctrl.verifyEmailToInvite(ctrl.newInvite);
        if (invite === true) {
            console.log('Email verified: checking if valid user');
            userService.otherDetails(ctrl.newInvite)
                .then(function(data) {
                        console.log("Invited player is a valid user! Storing invited user");
                        console.log(data.data);
                        ctrl.invitedUser = data.data;
                        if (ctrl.invitedUser != null) {
                            ctrl.makeGame();
                        }

                    },
                    function(err) {
                        console.log("Invited player doesn't exist" + err);
                    });
        } else {
            console.log("Invite user = null");
        }
    };
};


angular.module('Yahtzee')
    .component('playSection',
    {
        controller: PlaySectionController,
        templateUrl: 'Angular/Components/Profile/Play/play-section.html'
    });