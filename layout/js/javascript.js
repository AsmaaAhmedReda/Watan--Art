$( document ).ready(function() {
    $('.upload-btn').click(function() {
        $(this).parent().find('.two-btns').fadeIn('slow');
        $(this).hide();
        $(this).parent('.card-img').addClass('active');
    });

     $('.cancel-btn').click(function() {
         $(this).parents('.card').find('.two-btns').hide();
         $(this).parents('.card').find('.upload-btn').fadeIn();
         $(this).parents('.card-img').removeClass('active');
    });

    $(function() {

        $(".progress").each(function() {

            var value = $(this).attr('data-value');
            var left = $(this).find('.progress-left .progress-bar');
            var right = $(this).find('.progress-right .progress-bar');

            if (value > 0) {
            if (value <= 50) {
                right.css('transform', 'rotate(' + percentageToDegrees(value) + 'deg)')
            } else {
                right.css('transform', 'rotate(180deg)')
                left.css('transform', 'rotate(' + percentageToDegrees(value - 50) + 'deg)')
            }
            }

        })

        function percentageToDegrees(percentage) {
            return percentage / 100 * 360
        }
 
    });
    
});