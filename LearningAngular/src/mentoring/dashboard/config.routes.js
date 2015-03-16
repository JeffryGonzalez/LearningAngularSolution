(function () {
    angular.module('app.dashboard')
        .config([
            '$urlRouterProvider',
            '$stateProvider',
            function ($urlRouterProvider, $stateProvider) {
                $stateProvider.state("default", {
                    url: "/",
                    views: {
                        nav: {
                          templateUrl: "/src/mentoring/dashboard/nav.html"
                        },
                        header: {
                            template: "<h3>The Header</h3>"
                        },
                        content: {
                            templateUrl: "/src/mentoring/dashboard/content.html"
                        },
                        footer: {
                            template: "<p>Down in the footer, I hope!</p>"
                        }
                    }
                });

                $urlRouterProvider.otherwise('/');
            }
        ]);
})();