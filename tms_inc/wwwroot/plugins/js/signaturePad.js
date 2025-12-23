var $canvas,
    onResize = function (event) {
        let width;
        let heigth;
        if (window.innerWidth <= 375) {
            width = window.innerWidth;
            heigth = 150;
        }
        else {
            width = window.innerWidth - 600;
            heigth = 200;
        }

        $canvas.attr({
            height: heigth,
            width: width
        });
    };
$(document).ready(function () {
    $canvas = $('canvas');
    window.addEventListener('orientationchange', onResize, false);
    window.addEventListener('resize', onResize, false);
    onResize();

    $('.sigPad').signaturePad({
        drawOnly: true, drawBezierCurves: true, variableStrokeWidth: true, lineTop: 160, lineWidth: 3
    });


 
});
