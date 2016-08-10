var window_width = 0;
var window_height = 0;
var actionName = "";
var htmlstring = "";
var postBackData = "";
var allprojects = [];
var allpriorities = [];
var allstatus = [];
var allusers = [];
var ProjectWorkItems = [];
var chartData = [];
$(document).ready(function () {
    window_width = $(window).width() - 5;
    window_height = $(window).height() - 45;
    $(".mainmenucontent .parentmenu").live("mouseenter", function (e) {
        $(this).children(".popup").slideDown("slow");
    });
    $("#taskcontent").show();
    $(".mainmenucontent .parentmenu").live("mouseleave", function () {
        $(this).children(".popup").hide();
    }); 
    $("#imgproject").html("<img style=\"width:45px;\" src=\"/Images/profile/" + $("#Image_Path").val() + "\"/>");
    GetAllProjects();
    GetPriorities();
    GetStatuslist();
    GetUsers();
    $(".projects td").live("click", function () {
        ClosePopup();
        var lbltext = $(this).children("span").html().toLowerCase();
        switch (lbltext) {
            case "create project":
                {
                    $(".createproject").slideDown("slow");
                    break;
                }
            case "edit/remove project":
                {
                    GetProjects();
                    break;
                }
        }
    });
   
    $(".tasks td").live("click", function () {
        ClosePopup();
        var lbltext = $(this).children("span").html().toLowerCase();
        switch (lbltext) {
            case "create workitem":
                {
                    ShowDefaultWorkItem();
                    break;
                }
            case "create task":
                {
                    ShowDefaultTask();
                    break;
                }
            case "edit/remove workitem":
                {
                    GetWorkItems();
                    break;
                }
            case "edit/remove task":
                {
                    GetTaskDetails();
                    break;
                }
        }
    });
    $(document).on('keyup', ".txttaskcomplete", function () {
        $(this).parent(".username").parent("td").children(".taskcomplete").children(".taskpercentagecomplete").css("width", $(this).val() + "%");
    });
    $("#reports").click(function () {
        ClosePopup();
        $("#reportselectioncontent").show();
    });
});

function CreateNewProject()
{
    ClosePopup();
    jQuery.ajax({
        type: "POST",
        url: "/Home/CreateNewProject",
        async: true,
        data: "{ProjectName:'" + $("#txtprojectname").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null && content != 0)
                alert("Project was created successfully");
            else
                alert("Project was not created");
        },
        error: function (error) {
            alert("Project was not created");
        }
    });
 
}
function GetAllProjects() {
    ClosePopup();
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetAllProjects",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            $("#ddlProject").html("");
            if (content != null && content != "")
            {
                for (var i = 0; i < content.length; i++)
                {
                    allprojects.push({ ProjectId: content[i].ProjectId, ProjectName: content[i].ProjectName });
                    $("#ddlProject").append("<option value=\"" + content[i].ProjectId + "\">" + content[i].ProjectName + "</option>");
                }
            }
        },
        error: function (error) {

        }
    });
}
function GetPriorities() {
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetPriorities",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {           
            if (content != null && content != "") {
                for (var i = 0; i < content.length; i++) {
                    allpriorities.push({ PriorityId: content[i].PriorityId, PriorityName: content[i].PriorityName });                 
                }
                GetProjectWorkItems(content[0].PriorityId);
            }
        },
        error: function (error) {

        }
    });
}
function GetStatuslist() {
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetStatuslist",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null && content != "") {
                for (var i = 0; i < content.length; i++) {
                    allstatus.push({ StatusId: content[i].StatusId, StatusName: content[i].StatusName });
                }
            }
        },
        error: function (error) {

        }
    });
}
function GetUsers() {
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetUsers",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null && content != "") {
                for (var i = 0; i < content.length; i++) {
                    allusers.push({ UserId: content[i].UserId, UserName: content[i].UserName });
                }
            }
        },
        error: function (error) {

        }
    });
}
function LoadProjectWorkItems(obj)
{
    GetProjectWorkItems($(obj).val());
}
function GetProjectWorkItems(projectId) {
    ProjectWorkItems = [];
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetProjectWorkItems",
        async: true,
        data: "{ProjectId:" + projectId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            $("#ddlWorkItem").html("");
            if (content != null)
            {
                ProjectWorkItems = content;
                for (var i = 0; i < content.length; i++) {                
                    $("#ddlWorkItem").append("<option value=\"" + content[i].WorkItemId + "\">" + content[i].WorkItemName + "</option>");
                }
               
            }
        },
        error: function (error) {
           
        }
    });
    return ProjectWorkItems;
}
function GetProjects() {
    ClosePopup();
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetProjects",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            htmlstring = "<a onclick=\"ClosePopup();\" style=\"float: right; cursor: pointer;\" class=\"closepopup\"></a>";
            htmlstring += "<div id=\"editremoveproject\" style=\"margin-top:10px;\">";
            htmlstring += "<span class=\"title\">Edit/Remove Project</span>";
            htmlstring += "<div class=\"tableheader\" style=\"width: 480px; overflow: hidden;clear:both;\">";
            htmlstring += "<table>";
            htmlstring += "<tr>";
            htmlstring += "<td style=\"width:50px;\">S NO.</td>";
            htmlstring += "<td style=\"width:200px;\">Project Name</td>";
            htmlstring += "<td style=\"width:100px;\"></td>";
            htmlstring += "<td style=\"width:100px;\"></td>";
            htmlstring += "</tr>";
            htmlstring += "</table>";
            htmlstring += "</div>";
                         
            if (content != null && content != "")
            {
                htmlstring += "<div class=\"tablebody\" onscroll=\"ScrollTable(this);\" style=\"width: 480px;height:200px; overflow: auto;clear:both;\">";
                 htmlstring += "<table>";
                for (var i = 0; i < content.length; i++)
                {
                    htmlstring += "<tr>";
                    htmlstring += "<td class=\"tdId\" style=\"width:50px;\">" + content[i].RowNumber + "</td>";
                    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" disabled=\"true\" value=\"" + content[i].ProjectName + "\" style=\"height: 20px; width: 130px;\"/></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";                   
                    htmlstring += "<td class=\"tdEdit\" style=\"width:100px;\"><a value=\"" + content[i].ProjectId + "\" class=\"editremovelabels\" onclick=\"EditRemoveProject(this);\">Edit</a></td>";
                    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"" + content[i].ProjectId + "\" class=\"editremovelabels\" onclick=\"EditRemoveProject(this);\">Remove</a></td>";
                    htmlstring += "</tr>";
                }
                htmlstring += "</table>";
                htmlstring += "</div>";
                htmlstring += "</div>";
                $("#editremoveprojects").html(htmlstring);
                $("#editremoveprojects").slideDown("slow");
            }
        },
        error: function (error) {
           
        }
    });
}

function EditRemoveProject(obj) {
    var parentobj = $(obj).parent("td").parent("tr");    
    if ($(obj).html() == "Edit") {
        $(obj).html("Save");
        $(parentobj).children(".tdRemove").children("a").html("Cancel");
        EnableInputObjects(parentobj);        
    }
    else if ($(obj).html() == "Save") {
        postBackData = "{ProjectId:" + $(obj).attr("value") + "";
        postBackData += ",ProjectName:'" + $(parentobj).children(".tdProjectName").children(".username").children("input[type='text']").val() + "'}";
        jQuery.ajax({
            type: "POST",
            url: "/Home/EditProject",
            async: true,
            data: postBackData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (content) {
                DisableInputObjects(parentobj);
                $(parentobj).children(".tdRemove").children("a").html("Remove");
                $(parentobj).children(".tdEdit").children("a").html("Edit");
            },
            error: function (error) {

            }
        });
    }
    else if ($(obj).html() == "Remove") {
        var msgconfirm = window.confirm("Are you sure want to remove project");
        if (msgconfirm) {
            postBackData = "{ProjectId:" + $(obj).attr("value") + "";
            postBackData += ",ProjectName:'" + $(parentobj).children(".tdProjectName").children(".username").children("input[type='text']").val() + "'}";
            jQuery.ajax({
                type: "POST",
                url: "/Home/RemoveProject",
                async: true,
                data: postBackData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (content) {
                    DisableInputObjects(parentobj);
                    $(parentobj).remove();                    
                },
                error: function (error) {

                }
            });
        }
    }
    else if ($(obj).html() == "Cancel")
    {
        DisableInputObjects(parentobj);
        $(parentobj).children(".tdRemove").children("a").html("Remove");
        $(parentobj).children(".tdEdit").children("a").html("Edit");
    }
}

function ShowDefaultWorkItem() {
    htmlstring = "<a onclick=\"ClosePopup();\" style=\"float: right; cursor: pointer;\" class=\"closepopup\"></a>";
    htmlstring += "<div id=\"createnewworkitem\" style=\"margin-top:10px;\">";
    htmlstring += "<span class=\"title\">Create WorkItem</span>";
    htmlstring += "<div class=\"tableheader\" style=\"width: 580px; overflow: hidden;clear:both;\">";
    htmlstring += "<table>";
    htmlstring += "<tr>";
    htmlstring += "<td style=\"width:50px;\"></td>";
    htmlstring += "<td style=\"width:200px;\">Project Name</td>";
    htmlstring += "<td style=\"width:200px;\">WorkItem Name</td>";
    htmlstring += "<td style=\"width:100px;\"></td>";
    htmlstring += "</tr>";
    htmlstring += "</table>";
    htmlstring += "</div>";

    htmlstring += "<div class=\"tablebody\" onscroll=\"ScrollTable(this);\" style=\"width: 580px;height:200px;overflow: auto;clear:both;\">";
    htmlstring += "<table>";

    htmlstring += "<tr>";
    htmlstring += "<td class=\"tdId\" style=\"width:50px;\"><div onclick=\"AddWorkItem();\" class=\"additem\"></div></td>";
    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allprojects.length; i++)
    {
        htmlstring += "<option value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";    
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"\" class=\"editremovelabels\" onclick=\"RemoveCurrentRow(this);\">Remove</a></td>";
    htmlstring += "</tr>";

    htmlstring += "</table>";
    htmlstring += "</div>";

    htmlstring += "<div class=\"submit\">";
    htmlstring += " <input type=\"button\" value=\"Submit\" onclick=\"CreateWorkItem();\" />";
    htmlstring += "</div>";

    htmlstring += "</div>";
    $("#createworkitem").html(htmlstring);
    $("#createworkitem").slideDown("slow");
}
function AddWorkItem()
{
    htmlstring = "<tr>";
    htmlstring += "<td class=\"tdId\" style=\"width:50px;\"><div onclick=\"AddWorkItem();\" class=\"additem\"></div></td>";
    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allprojects.length; i++) {
        htmlstring += "<option value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"\" class=\"editremovelabels\" onclick=\"RemoveCurrentRow(this);\">Remove</a></td>";
    htmlstring += "</tr>";
    $('#createworkitem .tablebody tr:last').after(htmlstring);
}
function RemoveCurrentRow(obj) {
    if ($(obj).parent("td").parent("tr").parent("tbody").children("tr").length > 1)
        $(obj).parent("td").parent("tr").remove();
}
function CreateWorkItem()
{
    $("#createworkitem #createnewworkitem .tablebody tr").each(function (i) {
        CreateNewWorkItem($(this),i);
    });
}
function CreateNewWorkItem(parentobj,rowlength) {
    postBackData = "{ProjectID:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
    postBackData += ",WorkItemName:'" + $(parentobj).children(".tdWorkItemName").children(".username").children("input[type='text']").val() + "'}";
    jQuery.ajax({
        type: "POST",
        url: "/Home/CreateWorkItem",
        async: true,
        data: postBackData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null && content != 0 && ($("#createworkitem #createnewworkitem .tablebody tr").length) - 1 == rowlength)
                alert("WorkItem(s) was created successfully");         
        },
        error: function (error) {
            alert("WorkItem(s) was not created");
        }
    });
}
function EnableInputObjects(parentobj)
{  
    $(parentobj).children("td").find("input[type='text']").each(function () {
        $(this).removeAttr("disabled");
        $(this).css("border", "1px solid lightgrey");
    });

    $(parentobj).children("td").find("select").each(function () {
        $(this).removeAttr("disabled");      
    });
}
function DisableInputObjects(parentobj) {
    $(parentobj).children("td").find("input[type='text']").each(function () {
        $(this).attr("disabled", "true");
        $(this).css("border", "0");
    });
    $(parentobj).children("td").find("select").each(function () {
        $(this).attr("disabled", "true");      
    });
}
function ClosePopup() {
    $(".popupwindow").slideUp("slow");
    $("#reportselectioncontent").hide();
    $("#chartcontainer").hide();
    $("#chartdata").html("");   
}

function GetWorkItems() {
    ClosePopup();
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetWorkItems",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {          
            if (content != null) {
                htmlstring = "<a onclick=\"ClosePopup();\" style=\"float: right; cursor: pointer;\" class=\"closepopup\"></a>";
                htmlstring += "<div id=\"editremoveworkitem\" style=\"margin-top:10px;\">";
                htmlstring += "<span class=\"title\">Edit/Remove WorkItem</span>";
                htmlstring += "<div class=\"tableheader\" style=\"width: 680px; overflow: hidden;clear:both;\">";
                htmlstring += "<table>";
                htmlstring += "<tr>";
                htmlstring += "<td style=\"width:50px;\">S NO.</td>";
                htmlstring += "<td style=\"width:200px;\">Project Name</td>";
                htmlstring += "<td style=\"width:200px;\">WorkItem Name</td>";
                htmlstring += "<td style=\"width:100px;\"></td>";
                htmlstring += "<td style=\"width:100px;\"></td>";
                htmlstring += "</tr>";
                htmlstring += "</table>";
                htmlstring += "</div>";

                htmlstring += "<div class=\"tablebody\" onscroll=\"ScrollTable(this);\" style=\"width: 680px;height:200px;overflow: auto;clear:both;\">";
                htmlstring += "<table>";
                for (var i = 0; i < content.length; i++) {
                    htmlstring += "<tr>";
                    htmlstring += "<td class=\"tdId\" style=\"width:50px;\">" + content[i].RowNumber + "</td>";
                    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var j = 0; j < allprojects.length; j++) {
                        if (content[i].ProjectName == allprojects[j].ProjectName)
                            htmlstring += "<option selected=\"selected\" value=\"" + allprojects[j].ProjectId + "\">" + allprojects[j].ProjectName + "</option>";
                        else
                        htmlstring += "<option value=\"" + allprojects[j].ProjectId + "\">" + allprojects[j].ProjectName + "</option>";
                    }
                    htmlstring += "</select></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" disabled=\"true\" value=\"" + content[i].WorkItemName + "\" style=\"height: 20px; width: 130px;\"/></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdEdit\" style=\"width:100px;\"><a value=\"" + content[i].WorkItemId + "\" class=\"editremovelabels\" onclick=\"EditRemoveWorkItem(this);\">Edit</a></td>";
                    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"" + content[i].WorkItemId + "\" class=\"editremovelabels\" onclick=\"EditRemoveWorkItem(this);\">Remove</a></td>";
                    htmlstring += "</tr>";
                }
                htmlstring += "</table>";
                htmlstring += "</div>";             

                htmlstring += "</div>";
                $("#editremoveworkitems").html(htmlstring);
                $("#editremoveworkitems").slideDown("slow");
            }
        },
        error: function (error) {

        }
    });
}
function EditRemoveWorkItem(obj)
{
    var parentobj = $(obj).parent("td").parent("tr");
    if ($(obj).html() == "Edit") {
        $(obj).html("Save");
        $(parentobj).children(".tdRemove").children("a").html("Cancel");
        EnableInputObjects(parentobj);
    }
    else if ($(obj).html() == "Save") {
        postBackData = "{ProjectID:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
        postBackData += ",WorkItemId:" + $(obj).attr("value") + "";
        postBackData += ",WorkItemName:'" + $(parentobj).children(".tdWorkItemName").children(".username").children("input[type='text']").val() + "'}";
        jQuery.ajax({
            type: "POST",
            url: "/Home/EditWorkItem",
            async: true,
            data: postBackData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (content) {
                DisableInputObjects(parentobj);
                $(parentobj).children(".tdRemove").children("a").html("Remove");
                $(parentobj).children(".tdEdit").children("a").html("Edit");
            },
            error: function (error) {

            }
        });
    }
    else if ($(obj).html() == "Remove") {
        var msgconfirm = window.confirm("Are you sure want to remove project");
        if (msgconfirm) {
            postBackData = "{ProjectID:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
            postBackData += ",WorkItemId:" + $(obj).attr("value") + "";
            postBackData += ",WorkItemName:'" + $(parentobj).children(".tdWorkItemName").children(".username").children("input[type='text']").val() + "'}";
            jQuery.ajax({
                type: "POST",
                url: "/Home/RemoveWorkItem",
                async: true,
                data: postBackData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (content) {
                    DisableInputObjects(parentobj);
                    $(parentobj).remove();
                },
                error: function (error) {

                }
            });
        }
    }
    else if ($(obj).html() == "Cancel") {
        DisableInputObjects(parentobj);
        $(parentobj).children(".tdRemove").children("a").html("Remove");
        $(parentobj).children(".tdEdit").children("a").html("Edit");
    }

}

function ShowDefaultTask() {
    htmlstring = "<a onclick=\"ClosePopup();\" style=\"float: right; cursor: pointer;\" class=\"closepopup\"></a>";
    htmlstring += "<div id=\"createnewtask\" style=\"margin-top:10px;\">";
    htmlstring += "<span class=\"title\">Create Task</span>";
    htmlstring += "<div class=\"tableheader\" style=\"width: 760px; overflow: hidden;clear:both;\">";
    htmlstring += "<table style=\"width: 1500px;\">";
    htmlstring += "<tr>";
    htmlstring += "<td style=\"width:50px;\"></td>";
    htmlstring += "<td style=\"width:200px;\">Project Name</td>";
    htmlstring += "<td style=\"width:200px;\">WorkItem Name</td>";
    htmlstring += "<td style=\"width:200px;\">Task Name</td>";

    htmlstring += "<td style=\"width:200px;\">Effort</td>";
    htmlstring += "<td style=\"width:200px;\">Start Date</td>";
    htmlstring += "<td style=\"width:200px;\">End Date</td>";

    htmlstring += "<td style=\"width:200px;\">Assigned to</td>";
    htmlstring += "<td style=\"width:200px;\">Priority</td>";
    htmlstring += "<td style=\"width:200px;\">Status</td>";
    htmlstring += "<td style=\"width:200px;\">Completed</td>";
    htmlstring += "<td style=\"width:100px;\"></td>";
    htmlstring += "</tr>";
    htmlstring += "</table>";
    htmlstring += "</div>";

    htmlstring += "<div class=\"tablebody\" onscroll=\"ScrollTable(this);\" style=\"width: 760px;height:200px;overflow: auto;clear:both;\">";
    htmlstring += "<table style=\"width: 1500px;\">";

    htmlstring += "<tr>";
    htmlstring += "<td class=\"tdId\" style=\"width:50px;\"><div onclick=\"AddTask();\" class=\"additem\"></div></td>";

    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allprojects.length; i++) {
        htmlstring += "<option value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < ProjectWorkItems.length; i++) {
        htmlstring += "<option value=\"" + ProjectWorkItems[i].WorkItemId + "\">" + ProjectWorkItems[i].WorkItemName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdTaskName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdEffort\" style=\"width:200px;\">";
    htmlstring += "<span style=\"margin-right:2px;\">HH</span><input type=\"text\" value=\"\" style=\"height: 20px; width: 40px;border:1px solid lightgrey;margin-right:10px;\"/>";
    htmlstring += "<span style=\"margin-right:2px;\">mm</span><input type=\"text\" value=\"\" style=\"height: 20px; width: 40px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdStartDate\" style=\"width:200px;\">";
    htmlstring += "<input class=\"datepicker\" type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdEndDate\" style=\"width:200px;\">";
    htmlstring += "<input class=\"datepicker\" type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";


    htmlstring += "<td class=\"tdAssignedto\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allusers.length; i++) {
        htmlstring += "<option value=\"" + allusers[i].UserId + "\">" + allusers[i].UserName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdPriority\" style=\"width:200px;\">";
    htmlstring += "<div class=\"Priority\" style=\"background-color:" + SetPriorityColor(allpriorities[0].PriorityName) + ";\"></div><select onchange=\"SetPriority(this);\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allpriorities.length; i++) {
        htmlstring += "<option value=\"" + allpriorities[i].PriorityId + "\">" + allpriorities[i].PriorityName + "</option>";
    }
    htmlstring += "</select>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdStatus\" style=\"width:200px;\">";
    htmlstring += "<div class=\"Status\" style=\"background-color:" + SetStatusColor(allstatus[0].StatusName) + ";\"></div><select onchange=\"SetStatus(this);\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allstatus.length; i++) {
        htmlstring += "<option value=\"" + allstatus[i].StatusId + "\">" + allstatus[i].StatusName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";
    

    htmlstring += "<td class=\"tdComplete\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;\" class=\"username\"><input type=\"text\" class=\"txttaskcomplete\" value=\"0\" style=\"height: 20px; width: 30px;float:left;border: 1px solid lightgrey;\"/></div><div class=\"taskcomplete\" style=\"width:160px;height:23px;float:left;position:relative;right:-3px; overflow: hidden;\"><div class=\"taskpercentagecomplete\" style=\"width:0%;height:23px;background-color:green;float:left;\"></div></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"\" class=\"editremovelabels\" onclick=\"RemoveCurrentRow(this);\">Remove</a></td>";
    htmlstring += "</tr>";

    htmlstring += "</table>";
    htmlstring += "</div>";

    htmlstring += "<div class=\"submit\">";
    htmlstring += " <input type=\"button\" value=\"Submit\" onclick=\"CreateTask();\" />";
    htmlstring += "</div>";

    htmlstring += "</div>";
    $("#createtask").html(htmlstring);
    $("#createtask").slideDown("slow");
    $('.datepicker').datepicker();
}
function AddTask() {
    htmlstring = "<tr>";
    htmlstring += "<td class=\"tdId\" style=\"width:50px;\"><div onclick=\"AddTask();\" class=\"additem\"></div></td>";

    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allprojects.length; i++) {
        htmlstring += "<option value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < ProjectWorkItems.length; i++) {
        htmlstring += "<option value=\"" + ProjectWorkItems[i].WorkItemId + "\">" + ProjectWorkItems[i].WorkItemName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdTaskName\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdEffort\" style=\"width:200px;\">";
    htmlstring += "<span style=\"margin-right:2px;\">HH</span><input type=\"text\" value=\"\" style=\"height: 20px; width: 40px;border:1px solid lightgrey;margin-right:10px;\"/>";
    htmlstring += "<span style=\"margin-right:2px;\">mm</span><input type=\"text\" value=\"\" style=\"height: 20px; width: 40px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdStartDate\" style=\"width:200px;\">";
    htmlstring += "<input class=\"datepicker\" type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdEndDate\" style=\"width:200px;\">";
    htmlstring += "<input class=\"datepicker\" type=\"text\" value=\"\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\"/>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdAssignedto\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allusers.length; i++) {
        htmlstring += "<option value=\"" + allusers[i].UserId + "\">" + allusers[i].UserName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdPriority\" style=\"width:200px;\">";
    htmlstring += "<div class=\"Priority\" style=\"background-color:" + SetPriorityColor(allpriorities[0].PriorityName) + ";\"></div><select onchange=\"SetPriority(this);\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allpriorities.length; i++) {
        htmlstring += "<option value=\"" + allpriorities[i].PriorityId + "\">" + allpriorities[i].PriorityName + "</option>";
    }
    htmlstring += "</select></div>";
    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdStatus\" style=\"width:200px;\">";
    htmlstring += "<div class=\"Status\" style=\"background-color:" + SetStatusColor(allstatus[0].StatusName) + ";\"></div><select onchange=\"SetStatus(this);\" style=\"height: 20px; width: 130px;border:1px solid lightgrey;\">"
    for (var i = 0; i < allstatus.length; i++) {
        htmlstring += "<option value=\"" + allstatus[i].StatusId + "\">" + allstatus[i].StatusName + "</option>";
    }
    htmlstring += "</select>";
    htmlstring += "</td>";


    htmlstring += "<td class=\"tdComplete\" style=\"width:200px;\">";
    htmlstring += "<div style=\"float:left;\" class=\"username\"><input type=\"text\" class=\"txttaskcomplete\" value=\"0\" style=\"height: 20px; width: 30px;float:left;border: 1px solid lightgrey;\"/></div><div class=\"taskcomplete\" style=\"width:160px;height:23px;float:left;position:relative;right:-3px; overflow: hidden;\"><div class=\"taskpercentagecomplete\" style=\"width:0%;height:23px;background-color:green;float:left;\"></div></div>";
    htmlstring += "</td>";

    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"\" class=\"editremovelabels\" onclick=\"RemoveCurrentRow(this);\">Remove</a></td>";
    htmlstring += "</tr>";
    $('#createtask .tablebody tr:last').after(htmlstring);
    $('.datepicker').datepicker();
}
function ScrollTable(e) {
    $(e).parent("div").children(".tableheader").scrollTop(e.scrollTop);
    $(e).parent("div").children(".tableheader").scrollLeft(e.scrollLeft);
}

function CreateTask() {
    $("#createtask #createnewtask .tablebody tr").each(function (i) {
        CreateNewTask($(this), i);
    });
}
function CreateNewTask(parentobj, rowlength) {
    postBackData = "{ProjectId:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
    postBackData += ",WorkItemId:" + $(parentobj).children(".tdWorkItemName").children(".username").children("select").val() + "";
    postBackData += ",PriorityId:" + $(parentobj).children(".tdPriority").children("select").val() + "";
    postBackData += ",StatusId:" + $(parentobj).children(".tdStatus").children("select").val() + "";
    postBackData += ",TaskName:'" + $(parentobj).children(".tdTaskName").children(".username").children("input[type='text']").val() + "'";
    postBackData += ",AssignedTo:" + $(parentobj).children(".tdAssignedto").children(".username").children("select").val() + "";
    postBackData += ",Completed:" + $(parentobj).children(".tdComplete").children(".username").children("input[type='text']").val() + "}";
    jQuery.ajax({
        type: "POST",
        url: "/Home/CreateTask",
        async: true,
        data: postBackData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null && content != 0 && ($("#createtask #createnewtask .tablebody tr").length) - 1 == rowlength)
                alert("Task(s) was created successfully");
        },
        error: function (error) {
            alert("Task(s) was not created");
        }
    });
}

function GetTaskDetails() {
    ClosePopup();
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetTaskDetails",
        async: true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            if (content != null) {
                htmlstring = "<a onclick=\"ClosePopup();\" style=\"float: right; cursor: pointer;\" class=\"closepopup\"></a>";
                htmlstring += "<div id=\"createnewtask\" style=\"margin-top:10px;\">";
                htmlstring += "<span class=\"title\">Edit/Remove Task</span>";
                htmlstring += "<div class=\"tableheader\" style=\"width: 760px; overflow: hidden;clear:both;\">";
                htmlstring += "<table style=\"width: 1500px;\">";
                htmlstring += "<tr>";               
                htmlstring += "<td style=\"width:50px;\">Task #</td>";
                htmlstring += "<td style=\"width:200px;\">Project Name</td>";
                htmlstring += "<td style=\"width:200px;\">WorkItem Name</td>";
                htmlstring += "<td style=\"width:200px;\">Task Name</td>";
                htmlstring += "<td style=\"width:200px;\">Assigned to</td>";
                htmlstring += "<td style=\"width:200px;\">Priority</td>";
                htmlstring += "<td style=\"width:200px;\">Status</td>";
                htmlstring += "<td style=\"width:200px;\">Completed</td>";
                htmlstring += "<td style=\"width:100px;\"></td>";
                htmlstring += "<td style=\"width:100px;\"></td>";
                htmlstring += "</tr>";
                htmlstring += "</table>";
                htmlstring += "</div>";

                htmlstring += "<div class=\"tablebody\" onscroll=\"ScrollTable(this);\" style=\"width: 760px;height:200px;overflow: auto;clear:both;\">";
                htmlstring += "<table style=\"width: 1500px;\">";
           
                for (var j = 0; j < content.length; j++) {
                    htmlstring += "<tr>";
                    htmlstring += "<td class=\"tdId\" style=\"width:50px;\">" + content[j].TaskID + "</td>";

                    htmlstring += "<td class=\"tdProjectName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var i = 0; i < allprojects.length; i++) {
                        if (content[j].ProjectName == allprojects[i].ProjectName)
                            htmlstring += "<option selected=\"selected\" value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";
                        else
                        htmlstring += "<option value=\"" + allprojects[i].ProjectId + "\">" + allprojects[i].ProjectName + "</option>";
                    }
                    htmlstring += "</select></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdWorkItemName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var i = 0; i < ProjectWorkItems.length; i++) {
                        if (content[j].WorkItemName == ProjectWorkItems[i].WorkItemName)
                            htmlstring += "<option selected=\"selected\" value=\"" + ProjectWorkItems[i].WorkItemId + "\">" + ProjectWorkItems[i].WorkItemName + "</option>";
                        else
                        htmlstring += "<option value=\"" + ProjectWorkItems[i].WorkItemId + "\">" + ProjectWorkItems[i].WorkItemName + "</option>";
                    }
                    htmlstring += "</select></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdTaskName\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><input type=\"text\" disabled=\"true\" value=\"" + content[j].TaskName + "\" style=\"height: 20px; width: 130px;\"/></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdAssignedto\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;width:80%;\" class=\"username\"><select disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var i = 0; i < allusers.length; i++) {
                        if (content[j].UserName == allusers[i].UserName)
                            htmlstring += "<option selected=\"selected\" value=\"" + allusers[i].UserId + "\">" + allusers[i].UserName + "</option>";
                        else
                        htmlstring += "<option value=\"" + allusers[i].UserId + "\">" + allusers[i].UserName + "</option>";
                    }
                    htmlstring += "</select></div>";
                    htmlstring += "<div style=\"float:right;width:5%;color:red;\" class=\"invaliduser\"><span title=\"Invalid User Name\" class=\"errormsg\"></span></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdPriority\" style=\"width:200px;\">";
                    htmlstring += "<div class=\"Priority\" style=\"background-color:" + SetPriorityColor(content[j].PriorityName) + "\"></div><select onchange=\"SetPriority(this);\" disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var i = 0; i < allpriorities.length; i++) {
                        if (content[j].PriorityName == allpriorities[i].PriorityName)
                            htmlstring += "<option selected=\"selected\" value=\"" + allpriorities[i].PriorityId + "\">" + allpriorities[i].PriorityName + "</option>";
                        else
                        htmlstring += "<option value=\"" + allpriorities[i].PriorityId + "\">" + allpriorities[i].PriorityName + "</option>";
                    }
                    htmlstring += "</select>";                   
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdStatus\" style=\"width:200px;\">";
                    htmlstring += "<div class=\"Status\" style=\"background-color:" + SetStatusColor(content[j].StatusName) + "\"></div><select onchange=\"SetStatus(this);\" disabled=\"true\" style=\"height: 20px; width: 130px;\">"
                    for (var i = 0; i < allstatus.length; i++) {
                        if (content[j].StatusName == allstatus[i].StatusName)
                            htmlstring += "<option selected=\"selected\" value=\"" + allstatus[i].StatusId + "\">" + allstatus[i].StatusName + "</option>";
                        else
                        htmlstring += "<option value=\"" + allstatus[i].StatusId + "\">" + allstatus[i].StatusName + "</option>";
                    }
                    htmlstring += "</select>"
                    htmlstring += "</td>";


                    htmlstring += "<td class=\"tdComplete\" style=\"width:200px;\">";
                    htmlstring += "<div style=\"float:left;\" class=\"username\"><input type=\"text\" class=\"txttaskcomplete\" disabled=\"true\" value=\"" + content[j].Complete + "\" style=\"height: 20px; width: 30px;float:left;\"/></div><div class=\"taskcomplete\" style=\"width:160px;height:23px;float:left;position:relative;right:-3px; overflow: hidden;\"><div class=\"taskpercentagecomplete\" style=\"width:" + content[j].Complete + "%;height:23px;background-color:green;float:left;\"></div></div>";
                    htmlstring += "</td>";

                    htmlstring += "<td class=\"tdEdit\" style=\"width:100px;\"><a value=\"" + content[j].TaskID + "\" class=\"editremovelabels\" onclick=\"EditRemoveTask(this);\">Edit</a></td>";
                    htmlstring += "<td class=\"tdRemove\" style=\"width:100px;\"><a value=\"" + content[j].TaskID + "\" class=\"editremovelabels\" onclick=\"EditRemoveTask(this);\">Remove</a></td>";
                    htmlstring += "</tr>";
                }              

                htmlstring += "</table>";
                htmlstring += "</div>";

                htmlstring += "</div>";
                $("#editremovetaskdetails").html(htmlstring);
                $("#editremovetaskdetails").slideDown("slow");
            }
        },
        error: function (error) {

        }
    });
}

function SetPriorityColor(Priority) {
    switch (Priority) {
        case "Normal":
            {
                return "yellow";
                break;
            }
        case "Low":
            {
                return "green";
                break;
            }
        case "High":
            {
                return "red";
                break;
            }
        default:
            {
                return "transparent";
                break;
            }
    }
}

function SetStatusColor(Status) {
    switch (Status) {
        case "Not Started":
            {
                return "skyblue";
                break;
            }
        case "In Progress":
            {
                return "yellow";
                break;
            }
        case "Done":
            {
                return "green";
                break;
            }
        case "Cancelled":
            {
                return "red";
                break;
            }
        default:
            {
                return "transparent";
                break;
            }
    }
}

function EditRemoveTask(obj)
{
    var parentobj = $(obj).parent("td").parent("tr");
    if ($(obj).html() == "Edit") {
        $(obj).html("Save");
        $(parentobj).children(".tdRemove").children("a").html("Cancel");
        EnableInputObjects(parentobj);
    }
    else if ($(obj).html() == "Save") {
        postBackData = "{TaskId:" + $(obj).attr("value") + "";
        postBackData += ",ProjectId:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
        postBackData += ",WorkItemId:" + $(parentobj).children(".tdWorkItemName").children(".username").children("select").val() + "";
        postBackData += ",PriorityId:" + $(parentobj).children(".tdPriority").children("select").val() + "";
        postBackData += ",StatusId:" + $(parentobj).children(".tdStatus").children("select").val() + "";
        postBackData += ",TaskName:'" + $(parentobj).children(".tdTaskName").children(".username").children("input[type='text']").val() + "'";
        postBackData += ",AssignedTo:" + $(parentobj).children(".tdAssignedto").children(".username").children("select").val() + "";
        postBackData += ",Completed:" + $(parentobj).children(".tdComplete").children(".username").children("input[type='text']").val() + "}";
        jQuery.ajax({
            type: "POST",
            url: "/Home/EditTask",
            async: true,
            data: postBackData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (content) {
                DisableInputObjects(parentobj);
                $(parentobj).children(".tdRemove").children("a").html("Remove");
                $(parentobj).children(".tdEdit").children("a").html("Edit");
            },
            error: function (error) {

            }
        });
    }
    else if ($(obj).html() == "Remove") {
        var msgconfirm = window.confirm("Are you sure want to remove project");
        if (msgconfirm) {
            postBackData = "{TaskId:" + $(obj).attr("value") + "";
            postBackData += ",ProjectId:" + $(parentobj).children(".tdProjectName").children(".username").children("select").val() + "";
            postBackData += ",WorkItemId:" + $(parentobj).children(".tdWorkItemName").children(".username").children("select").val() + "";
            postBackData += ",PriorityId:" + $(parentobj).children(".tdPriority").children("select").val() + "";
            postBackData += ",StatusId:" + $(parentobj).children(".tdStatus").children("select").val() + "";
            postBackData += ",TaskName:'" + $(parentobj).children(".tdTaskName").children(".username").children("input[type='text']").val() + "'";
            postBackData += ",AssignedTo:" + $(parentobj).children(".tdAssignedto").children(".username").children("select").val() + "";
            postBackData += ",Completed:" + $(parentobj).children(".tdComplete").children(".username").children("input[type='text']").val() + "}";
            jQuery.ajax({
                type: "POST",
                url: "/Home/RemoveTask",
                async: true,
                data: postBackData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (content) {
                    DisableInputObjects(parentobj);
                    $(parentobj).remove();
                },
                error: function (error) {

                }
            });
        }
    }
    else if ($(obj).html() == "Cancel") {
        DisableInputObjects(parentobj);
        $(parentobj).children(".tdRemove").children("a").html("Remove");
        $(parentobj).children(".tdEdit").children("a").html("Edit");
    }

}
function SetPriority(obj)
{  
    $(obj).parent("td").children(".Priority").css("background-color",SetPriorityColor($(obj).children(':selected').text()));
}
function SetStatus(obj) {
    $(obj).parent("td").children(".Status").css("background-color", SetStatusColor($(obj).children(':selected').text()));
}
function GenerateReport()
{   
    $("#chartdata").html("");
    $("#chartcontainer").show();
    postBackData = "{ProjectId:" + $("#ddlProject").val() + "";
    postBackData += ",WorkItemId:" + $("#ddlWorkItem").val() + "}";
    jQuery.ajax({
        type: "POST",
        url: "/Home/GetWorkItemTaskDetails",
        async: true,
        data: postBackData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (content) {
            PlotPieChart(content);
        },
        error: function (error) {

        }
    });
}
function PlotPieChart(content) {
    // Build the chart
    chartData = [];
    for (var i = 0; i < content.length; i++) {
        var chartparam = content[i];
        var chartvalues = [];
        chartvalues.push(chartparam.TaskName, chartparam.Complete);
        chartData.push(chartvalues);
    }
    $('#chartdata').highcharts({
        chart: {
            plotBackgroundColor: "rgb(235, 233, 236)",
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: ''
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.percentage:.1f}%',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: 'Usage',
            data: chartData
        }],
        colors: GetChartColors()
    });
}
function GetChartColors() {
    var chartcolors = ["#E41E2B", "#D2D9DF", "#31859C", "#FFC000", "#00B050", "#7030A0", "#7F7F7F", "#C00000", "#0070C0", "#FF9900", "#000000", "#838C87", "#83E5BB"];
    return chartcolors;
}