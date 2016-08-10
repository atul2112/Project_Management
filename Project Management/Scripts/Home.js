var htmlstring = "";
var postBackData = "";
var ProjectId = "";
var ProjectName = "";
var Image_Path = "";

$(document).ready(function () {
    GetProjects();
    Sample();
    $("#Projects .slide").live("click", function () {
         ProjectId = $(this).children(".projectname").attr("projectid");
         ProjectName = $(this).children(".projectname").attr("projectname");
         Image_Path = $(this).children(".projectname").attr("image_path");
        var lbltext = $(this).children(".projectname").html().toLowerCase();
        StoreProjectDetails();        
    });  
});
function Sample() { };
function StoreProjectDetails() {
    postBackData = "{ProjectId: " + ProjectId + "";
    postBackData += ",ProjectName: '" + ProjectName + "'";
    postBackData += ",Image_Path:'" + Image_Path + "'}";
    jQuery.ajax({
        type: "POST",
        url: "/Home/StoreProjectDetails",
        async: true,
        data: postBackData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            window.location = "/Home/Task";
        },
        error: function (error) {
            alert(error.responseText);
        }
    });
}
function GetProjects() {
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetProjects",
        async: false,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            htmlstring = "";
            for (var i = 0; i < content.length; i++) {
                htmlstring += "<div class=\"slide\">";
                htmlstring += "<img src=\"/Images/profile/" + content[i].Image_Path + "\"/><div projectid=\"" + content[i].ProjectId + "\" projectname=\"" + content[i].ProjectName + "\" image_path=\"" + content[i].Image_Path + "\" class=\"projectname\">" + content[i].ProjectName + "</div>"
                htmlstring += "</div>";
            }
            $('.jR3DCarouselGallery1').html("");
            $('.jR3DCarouselGallery1').html(htmlstring);
            BuildSlider();
        },
        error: function (error) {

        }
    });
}

function BuildSlider() {
    var myjR3DCarousel = $('.jR3DCarouselGallery1').jR3DCarousel({
        width: 600, 				// largest allowed width
        height: 322, 				// largest allowed height
        slideLayout: 'contain',    // "contain" (fit according to aspect ratio), "fill" (stretches object to fill) and "cover" (overflows box but maintains ratio)
        animation: 'slide3D', // slide | slide3D | scroll | scroll3D | fade       
        animationDuration: 2000,
        animationInterval: 1000,
        autoplay: null,
        onSlideShow: shown,
        navigation: 'circles', // circles | squares */
        slides: []
        // array of images source or slides from template
    });
    myjR3DCarousel.showSlide(0);
    myjR3DCarousel.showPreviousSlide();
    myjR3DCarousel.showNextSlide();
    var slide = myjR3DCarousel.getSlideByIndex(1);
    var currentSlide = myjR3DCarousel.getCurrentSlide();
}
function shown(slide) {

}