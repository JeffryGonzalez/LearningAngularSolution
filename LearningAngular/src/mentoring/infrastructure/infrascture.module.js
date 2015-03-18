(function () {
    angular.module("app.infrastructure", [
        // angular modules
        'ui.router',
        'ngAnimate',
        'ngMessages'
        // cross-cutting concerns (logging, etc.)

        // 3rd party modules
    ]);
})();

(function () {
    angular.module("app.infrastructure").factory("formatters", formatters);

    // inject

    function formatters() {
        var service = {
            formatName: formatName
        };

        return service;

        function formatName(first, last) {
            return last + ', ' + first;
        }
    }
})();