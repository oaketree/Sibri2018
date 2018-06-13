module.exports = function (grunt) {
    grunt.initConfig({
        jshint: {
            files: ['wwwroot/js/*.js'],
            options: {
                '-W069': false,
            }
        }, babel: {
            options: {
                sourceMap: false,
                presets: ['env']
            },
            dist: {
                files: {'wwwroot/js/es5/axios.min.js': 'wwwroot/js/axios.min.js'}
            }
        },
    });
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-babel');
    //grunt.registerTask('default', ['babel']);
};