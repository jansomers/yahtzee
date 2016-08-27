function ProfileController(userService, $rootScope) {
   
    var ctrl = this;
    ctrl.user = $rootScope.user;
    ctrl.isUserSectionShown = true;
    ctrl.isPlaySectionShown = true;
    ctrl.isGamesSectionShown = true;
    ctrl.isLoadedSectionShown = true;

    ctrl.getFullUser = function () {
        return $rootScope.fullUser;
    };

    ctrl.noSectionsShown = function() {
        return !(ctrl.isUserSectionShown ||
            ctrl.isPlaySectionShown ||
            ctrl.isGamesSectionShown ||
            ctrl.isLoadedSectionShown);
    };

    ctrl.loadedGame = function() {
        return $rootScope.loadedGame;
    };

    ctrl.shouldShowLoaded = function() {
        return (ctrl.loadedGame() != null);
    };

    ctrl.wonGames = function() {
        return $rootScope.stats.wins;
    };

    ctrl.bestScore = function() {
        return $rootScope.stats.best;
    };
    ctrl.avgScore = function() {
        return $rootScope.stats.average;
    };

};

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

angular.module('Yahtzee').component('profile', {

    controller: ProfileController,
    templateUrl: 'Angular/Components/Profile/profile.html'
});