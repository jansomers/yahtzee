function TabPanelController(loginService, userService, $rootScope, $anchorScroll) {
    var ctrl = this;
    ctrl.tab = null;

    ctrl.setTab = function (setTab) {
        $anchorScroll();
        if (setTab === 4) {
            ctrl.updateFullUser();
        }
        ctrl.tab = setTab;
    };
    ctrl.getFullUser = function() {
        return $rootScope.fullUser;
    };
    ctrl.updateFullUser = function() {
        $rootScope.updateFullUser();
    };
    ctrl.isSet = function(tab) {
        return ctrl.tab === tab;
    };

    ctrl.isLoggedin = function() {
        return $rootScope.isLoggedin();
    }

    ctrl.isLoggedinAs = function() {
        return $rootScope.user;
    };
    ctrl.logout = function () {
        console.log("Logged out: " + ctrl.isLoggedin());
        loginService.logout();
    };
    /**
     * Setting tab to homepage
     */
    ctrl.setTab(1);
};

angular.module('Yahtzee')
    .component('tabPanel',
    {
        controller: TabPanelController,
        templateUrl: '/Angular/Components/panel/tab-panel.html'

    });