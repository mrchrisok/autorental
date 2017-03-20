<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SandboxProcessing.aspx.cs" Inherits="SandboxUI.Sandbox.SandboxProcessing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form Processing</title>
    <link href="sandbox.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.3.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" method="post" runat="server" class="form-horizontal">

        <fieldset class="col-sm-6">
            <legend>Basic Info</legend>
            <div class="form-group">
                <label for="fullname" class="col-sm-3 control-label">Name</label>
                <div class="col-sm-9">
                    <input type="text" id="fullname" name="fullname" class="form-control" value="" />
                </div>
            </div>

            <div class="form-group">
                <label for="description" class="col-sm-3 control-label">Description</label>
                <div class="col-sm-9">
                    <textarea id="description" name="description" class="form-control" rows="3" cols="40"></textarea>
                </div>
            </div>

            <div class="form-group">
                <label for="employment-status" class="col-sm-3 control-label">Current Employment</label>
                <div class="col-sm-9">
                    <select id="employment-status" name="employment-status" class="col-sm-10 form-control">
                        <option>Full Time</option>
                        <option>Part Time</option>
                        <option>Unemployed</option>
                        <option>Retired</option>
                    </select>
                </div>
            </div>
        </fieldset>

        <div class="col-sm-6">
            <fieldset>
                <legend>Skills</legend>
                <div class="form-group">
                    <div class="col-sm-offset-1">
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" name="indentation" value="indentation" />Perfect source code indentation skills</label>
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" name="fast" value="fast" />Types real fast with 2 fingers</label>
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" name="git" value="git" />Keeps resume in a GIT repository</label>
                        </div>
                    </div>
                </div>
            </fieldset>

            <fieldset>
                <legend>Major Quirk</legend>
                <div class="form-group">
                    <div class="col-sm-offset-1">
                        <div class="radio">
                            <label>
                                <input type="radio" name="quirk" value="ruby"  />
                                Pretends to know Ruby</label><br />
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="quirk" value="long" />
                                Creates massively long variable names</label><br />
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="quirk" value="suit" />
                                Always wears a suit</label><br />
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="quirk" value="donno" checked="checked" />
                                To be discovered...</label><br />
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>

        <button type="submit" class="btn btn-primary">Process</button>

    </form>

    <script>

        // asynchronous processing
        // oco: async postback will not wipe the client controls
        // values from custom controls can be added prior to async post
        // promise functions can be used to add success/fail behavior to the page
        $(function () {
            $('form').submit(function (event) {
                alert('asynchronous postback!');

                event.preventDefault(); // suppress the postback

                // serialize the form
                var formData = $('form').serializeArray();

                // add additional values to the formData array
                // values from custom non-serializable controls (ex. Google maps) can be added this way
                formData.push({
                    name: 'bonus',
                    value: 'hamburger'
                });

                // postback the data
                $.post('SandBoxProcessing.aspx', formData)
                    .done(function () {
                        alert('success');
                    })
                    .fail(function () {
                        alert('fail');
                    })
                    .always(function () {
                        alert('response ')
                    })
            });
        });

        //// asynchronous processing
        //// oco: async postback will not wipe the client controls
        //// values from custom controls can be added prior to async post
        //$(function () {
        //    $('form').submit(function (event) {
        //        alert('asynchronous postback!');

        //        event.preventDefault(); // suppress the postback

        //        // serialize the form
        //        var formData = $('form').serializeArray();

        //        // add additional values to the formData array
        //        // values from custom non-serializable controls (ex. Google maps) can be added this way
        //        formData.push({
        //            name: 'bonus',
        //            value: 'hamburger'
        //        });

        //        // postback the data
        //        $.post('SandBoxProcessing.aspx', formData);
        //    });
        //});


        //// asynchronous processing
        //// oco: async postback will not wipe the client controls
        //$(function () {
        //    $('form').submit(function (event) {
        //        alert('asynchronous postback!');

        //        event.preventDefault(); // suppress the postback
                
        //        // serialize the form
        //        var formData = $('form').serializeArray();
        //        
        //        // postback the data
        //        $.post('SandBoxProcessing.aspx', formData);
        //    });
        //});


        //// synchronous processing
        // oco: sync postback will not wipe the client controls
        //$(function () {
        //    $('form').submit(function (event) {
        //        alert('synchronous postback!');
        //    });
        //});

    </script>

</body>
</html>

