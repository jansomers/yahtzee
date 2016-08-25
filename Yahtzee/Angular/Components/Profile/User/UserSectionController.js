function UserSectionController(userService, $rootScope) {
    var ctrl = this;

    ctrl.newScreenName = null;
    ctrl.newAvatar = null;
    ctrl.getFullUser = function() {
        return $rootScope.fullUser;
    };

    ctrl.hasScreenName = function () {
        console.log('Checking if user has a screenName');
        return ctrl.getFullUser().screenName != null;
    };
    ctrl.hasAvatar = function () {
        console.log('Checking if user has an avatar');
        return ctrl.getFullUser().avatar != null;
    };

    ctrl.verifyScreenName = function() {
        console.log('Verifying new screen name');
        var change = false;
        if (ctrl.newScreenName != null && ctrl.newScreenName.length > 5) {
            change = true;
        }
        return change;
    };

    ctrl.verifyAvatar = function() {
        console.log('Verifying new avatar');
        var change = false;
        if (ctrl.newAvatar != null) {
            change = true;
        }
        return change;
    }

    ctrl.changeScreenName = function() {
        console.log('Requested to change screen name');
        var change = ctrl.verifyScreenName();
        if (change === true) {
            console.log('Screen name verified');
            userService.updateScreenName(ctrl.newScreenName)
                .then(function() {
                    console.log('Succesfully changed screen name');
                    $rootScope.updateFullUser();
                });
        }
    };

    ctrl.changeAvatar = function() {
        console.log('Request to change avatar');
        var change = ctrl.verifyAvatar();
        if (change === true) {
            console.log('Avatar verified');
            userService.updateAvatar(ctrl.newAvatar)
                .then(function() {
                    console.log('Succesfully changed avatar');
                    $rootScope.updateFullUser();
                });
        }
    };


};




angular.module('Yahtzee')
    .component('userSection',
        {
            controller: UserSectionController,
            templateUrl: 'Angular/Components/Profile/User/user-section.html'
        }
    );