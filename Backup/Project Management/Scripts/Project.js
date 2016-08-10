
$(document).ready(function () {
    ClosePopup();
    $("#addproject").click(function () {
        $(".popupwindow:not(.createproject)").slideUp("slow");
        $(".createproject").slideDown("slow");
    });
    $("#projectsummary").click(function () {
        $(".popupwindow:not(#projectsummary)").slideUp("slow");
        GetProjects();
    });
    $("#addteammember").click(function () {
        $(".popupwindow:not(.createteammember)").slideUp("slow");
        $(".createteammember").slideDown("slow");     
    });
});
function CreateNewProject()
{
    if ($('#txtprojectname').val() == "")
    {
        alert("Please enter project name");
        return;
    }
    var postdata = $('#ProjectMaster').serialize();
    var file = document.getElementById('files').files[0];
    var txtprojectname = $('#txtprojectname').val();
    var fd = new FormData();
    fd.append("files", file);
    fd.append("txtprojectname", txtprojectname);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Home/UploadImage", false);
    xhr.send(fd);   
}
function ClosePopup() {   
    $(".popupwindow").slideUp("slow");
    $("#reportselectioncontent").hide();
    $("#chartcontainer").hide();
    $("#chartdata").html("");
}
function GetProjects() {
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetProjects",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            htmlstring = "";
            for (var i = 0; i < content.length; i++) {
                htmlstring += "<div class=\"slide\">";
                htmlstring += "<img src=\"/Images/profile/" + content[i].Image_Path + "\"/><div class=\"projectname\">" + content[i].ProjectName + "</div>"
                htmlstring += "</div>";
            }
            $('#projectsummaryreport').html("");
            $('#projectsummaryreport').html(htmlstring);           
            $('#projectsummaryreport').slideDown("slow");
        },
        error: function (error) {

        }
    });
}
function shown(slide) {

}