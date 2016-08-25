function HomePageController($rootScope) {
    var ctrl = this;

    ctrl.isLoggedIn = function() {
        return $rootScope.isLoggedin();
    };

}

angular.module('Yahtzee')
    .component('homepage',
    {
        controller: HomePageController,
        templateUrl: '/Angular/Components/Home/homepage.html'
    });