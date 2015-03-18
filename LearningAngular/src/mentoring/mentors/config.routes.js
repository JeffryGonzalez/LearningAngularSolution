(function () {
    angular.module("app.mentors")
        .config([
            '$urlRouterProvider',
            '$stateProvider',
            function ($urlRouterProvider, $stateProvider) {
	            $stateProvider
		            .state("default.mentors", {
			            url: "/list",
			            views: {
				            "content@": {
					            templateUrl: "/src/mentoring/mentors/list.html",
					            controller: "Mentors as vm"
				            },
				            "header@": {
					            template: "<h1>Mentor Information</h1>"
				            }
			            }
		            })
		            .state("default.mentors.new", {
			            url: "/new",
			            views: {
				            "content@": {
					            templateUrl: "/src/mentoring/mentors/new.html",
					            controller: "Mentors as vm"
				            },
				            "header@": {
					            template: "<h3>Create a New Mentor</h3>"
				            }
			            }
		            })
		            .state("default.mentors.show", {
		            	url: "/:id",
									views: {
										"content@": {
											template: "Showing a mentor"
										}
									}
		            });
            }
        ]);
})();

(function () {
    angular.module("app.mentors")
        .controller("Mentors", Mentors);

    Mentors.$inject = ['mentorsData','$scope','$state'];

    function Mentors(mentorsData,$scope,$state) {
        var vm = this;

        var newMentor = {
            firstName: "",
            lastName: "",
            email: "",
            available: true
        };

        vm.newMentor = angular.copy(newMentor);

        vm.add = function (mentor) {
            var mentorToAdd = angular.copy(mentor);
            mentorsData.add(mentorToAdd).then(
                function (addedMentor) {
                    console.log("You added a mentor as ", addedMentor.id);
                    vm.newMentor = angular.copy(newMentor);
                   
                    $state.go('default.mentors');
                }, function (error) {
                    alert(error);
                });
        };

        mentorsData.getAll().then(function (data) {
            vm.list = data;
        });

    }

})();

(function () {
    angular.module("app.mentors").factory("mentorsData", mentorsData);

    // inject
    mentorsData.$inject = ['$q', '$http', 'formatters','$interval'];

    function mentorsData($q, $http, formatters, $interval) {
        var service = {
            getAll: getAllMentors,
            add: add
        };

        $interval(function() {
            console.warn("invalidating the cache.");
            alreadyDownloaded = false;
        }, 2000);

        var cachedItems = [];
        var alreadyDownloaded = false;
        return service;

        function add(mentor) {
            var d = $q.defer();
            mentor = mentor || {};
            $http.post('/api/mentors', mentor).then(
                function (r) {
                    cachedItems.push(cleanUp(r.data));
                    d.resolve(r.data);
                },
                function (e) {
                    console.log(e);
                    if (e.status === 400) {
                        d.reject("Validation failed at the server");
                    }
                });
            return d.promise;
        }

        function getAllMentors() {
            var d = $q.defer();
            if (alreadyDownloaded) {
                d.resolve(cachedItems);
            } else {
                $http.get('/api/mentors').then(function(r) {
                    var vm = r.data.map(cleanUp);
                    cachedItems = vm;
                    alreadyDownloaded = true;
                    d.resolve(vm);
                });
            }
            return d.promise;
        }

        function cleanUp(m) {
            m.fullName = formatters.formatName(m.firstName, m.lastName);
            return m;
        }
    }
})();