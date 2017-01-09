$(function()
{

    if (!window['console'])
    {
        window.console = {};
        window.console.log = function(){};
    }

$('#date-range1').dateRangePicker(
{
    startOfWeek: 'monday',
    separator: ' ~ ',
    format: 'DD.MM.YYYY HH:mm',
    autoClose: false,
    showShortcuts: false,
    time: {
        enabled: true
    }
}).bind('datepicker-closed', function () {
    window.location.search = 'date=' + $('#date-range1').val();
});

});