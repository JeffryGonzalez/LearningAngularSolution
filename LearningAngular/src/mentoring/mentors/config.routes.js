(function() {
    angular.module("app.mentors")
        .config([
            '$urlRouterProvider',
            '$stateProvider',
            function($urlRouterProvider, $stateProvider) {
                $stateProvider.state("default.mentors", {
                    url: "mentors",
                    views: {
                        "content@": {
                            templateUrl: "/src/mentoring/mentors/content.html"
                        },
                        "header@": {
                            template: "<h1>Mentor Information</h1>"
                        }
                    }
                });
            }
        ]);
})();