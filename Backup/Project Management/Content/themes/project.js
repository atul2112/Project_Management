

    function saved_dialog_down() {
        $('#opt_dialog').css('background-color', '#009090');
        setTimeout(function () {
            $('#opt_dialog').css('-webkit-transform', 'perspective(400) rotate3d(0,0,0,0)')
            $('#main_container').animate({ 'padding-top': '90px' }, 400);
        },300);
    }

    function exist_dialog_down() {
        $('#opt_dialog').css('background-color', '#E8254B');
        setTimeout(function () {
            $('#opt_dialog').css('-webkit-transform', 'perspective(400) rotate3d(0,0,0,0)')
            $('#main_container').animate({ 'padding-top': '90px' }, 400);
        }, 300);
    }

    function dialog_up() {    
        setTimeout(function () {
            $('#opt_dialog').css('-webkit-transform', 'perspective(400) rotate3d(1,0,0,-90deg)')
            $('#main_container').animate({ 'padding-top': '45px' }, 400);
        }, 300);
    }

