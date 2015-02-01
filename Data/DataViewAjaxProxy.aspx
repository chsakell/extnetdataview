<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataViewAjaxProxy.aspx.cs" Inherits="Data.DataViewAjaxProxy" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="http://speed.ext.net/www/intro/css/main.css" />
    <title></title>
    <style type="text/css">
        .info {
            margin: 10px 0% 0 2%;
            padding: 4px;
            display: inline-block;
            width: 184px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-shadow: 2px 2px 2px #999;
        }

            .info:nth-child(odd) {
                background-color: #eee;
            }
    </style>
    <script type="text/javascript">
        var MyApp = {
            actor: {
                prepareActor: function (data) {
                    data.IsGoodActorText = data.IsGoodActor === true ? 'Yes' : 'No';
                    return data;
                },
                filter: function (text, store) {
                    store.filter('FirstName', text);
                }
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" Theme="Neptune" />
    <ext:Panel runat="server" Title="Actors" Icon="Group" AutoScroll="true" Height="450" Width="700">
        <Items>
            <ext:DataView runat="server" ItemSelector=".info">
                <Store>
                    <ext:Store ID="ActorsStore" runat="server" PageSize="5" RemoteSort="true" RemoteFilter="true">
                        <Model>
                            <ext:Model runat="server" IDProperty="Id">
                                <Fields>
                                    <ext:ModelField Name="Id" />
                                    <ext:ModelField Name="FirstName" />
                                    <ext:ModelField Name="LastName" />
                                    <ext:ModelField Name="DateOfBirth" Type="Date" />
                                    <ext:ModelField Name="Age" Type="Int" />
                                    <ext:ModelField Name="IsGoodActor" Type="Boolean" />
                                    <ext:ModelField Name="Image" Type="String" />
                                    <ext:ModelField Name="FamousFor" Type="String" />
                                </Fields>
                            </ext:Model>
                        </Model>
                        <Proxy>
                            <ext:AjaxProxy Url="CustomService.asmx/GetActors">
                                <ActionMethods Read="POST" />
                                <Reader>
                                    <ext:JsonReader Root="data" />
                                </Reader>
                            </ext:AjaxProxy>
                        </Proxy>
                    </ext:Store>
                </Store>
                <Tpl runat="server">
                    <Html>
                        <tpl for=".">
                        <div class="info">
                        <img src="/images/actors/{Image}.jpg" height="90" width="90" alt="Employee {Id}"/>
                        <div style="font-weight:bold">{FirstName} {LastName}</div>
                        <div>Dob: {DateOfBirth:date("m/d/Y")}</div>
                        <div>Age: {Age}</div>
                        <div>Is Good Actor? {IsGoodActorText}</div>
                        <div>Famous For <a href="#" target="_blank">{FamousFor}</a></div>
                        </div>
                        </tpl>
                    </Html>
                </Tpl>
                <PrepareData Fn="MyApp.actor.prepareActor" />
            </ext:DataView>
        </Items>
        <BottomBar>
            <ext:PagingToolbar runat="server" StoreID="ActorsStore" />
        </BottomBar>
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:ToolbarTextItem Text="Filter:" runat="server" />
                    <ext:TextField ID="FilterText" runat="server" EmptyText="Filter by first name" />
                    <ext:Button runat="server" Icon="Find">
                        <Listeners>
                            <Click Handler="MyApp.actor.filter(#{FilterText}.getValue(), #{ActorsStore});" />
                        </Listeners>
                    </ext:Button>
                    <ext:ToolbarSeparator runat="server" />
                    <ext:Button runat="server" Icon="BulletArrowDown" Text="Sort by LastName">
                        <Listeners>
                            <Click Handler="#{ActorsStore}.sort('LastName', 'DESC');" />
                        </Listeners>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </ext:Panel>
</body>
</html>
