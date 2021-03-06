$(document).ready(function () {
  $(".upload-btn").click(function () {
    $(this).parent().find(".two-btns").fadeIn("slow");
    $(this).hide();
    $(this).parent(".card-img").addClass("active");
    $(".item-details").hide();
  });

  $(".cancel-btn").click(function () {
    $(this).parents(".card").find(".two-btns").hide();
    $(this).parents(".card").find(".upload-btn").fadeIn();
    $(this).parents(".card-img").removeClass("active");
    $(".item-details").hide();
  });

  $(function () {
    $(".progress").each(function () {
      var value = $(this).attr("data-value");
      var left = $(this).find(".progress-left .progress-bar");
      var right = $(this).find(".progress-right .progress-bar");

      if (value > 0) {
        if (value <= 50) {
          right.css(
            "transform",
            "rotate(" + percentageToDegrees(value) + "deg)"
          );
        } else {
          right.css("transform", "rotate(180deg)");
          left.css(
            "transform",
            "rotate(" + percentageToDegrees(value - 50) + "deg)"
          );
        }
      }
    });

    function percentageToDegrees(percentage) {
      return (percentage / 100) * 360;
    }
  });

  //show / hide product edit div
  $(".item-details").hide();

  $("#T-shirt").click(function () {
    $("#1").toggle();
    $("#2").hide();
  });

  $("#Cover-iPad").click(function () {
    $("#2").toggle();
    $("#1").hide();
  });
  
  $(".Apply-btn").click(function () {
    $(this).parents(".item-details").hide();
  });


});
