<!DOCTYPE>

<html>

<head>
    <title>cose demo 676</title>

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://cytoscape.github.io/cytoscape.js/api/cytoscape.js-latest/cytoscape.min.js"></script>
    <script src="http://goessner.net/download/prj/jsonxml/json2xml.js"></script>

    <style>
        body {
            font-family: helvetica;
            font-size: 14px;
        }

        #cy {
            width: 80%;
            height: 100%;
            position: absolute;
            left: 260px;
            top: 0;
            z-index: 1;
        }

        h1 {
            color: white;
        }

        h2 {
            color: rgb(85, 85, 85);
        }

        .col-md-2 {
            position: absolute;
            z-index: 100;
            background-color: cadetblue;
            color: white;
            margin-top: -20px !important;
            padding-top: 0px !important;
            height: 420%;
        }
    </style>
</head>

<body>


<div class="col-md-2">
    <h3 id="nodeName"></h3>
    <h4 style="display: none" id="nodeId"></h4>

    <div class="form-group">
        <label for="actionName">action name:</label>
        <input type="text" class="form-control" name="actionName" id="actionName">
    </div>
    <div class="form-group">
        <label for="animationName">animation name:</label>
        <input type="text" class="form-control" id="animationName">
    </div>
    <h5>Position</h5>
    <div class="form-group">
        <div class="col-md-4">x<input id="x" class="form-control"  placeholder="x"> </div>
        <div class="col-md-4">y<input id="y" class="form-control"  placeholder="y"> </div>
        <div class="col-md-4">z<input id="z" class="form-control"  placeholder="z"> </div>
    </div>
    <div class="form-group">
        <label for="neighbours">neigbours:</label>
        <select class="form-control" id="neighbours">
            %for action in actions:
            <option value='{{action["data"]["id"]}}'>{{action["data"]["name"]}}</option>
            %end
        </select>
        <button onclick="addNeighbour()">+</button>

        <div class="neighbours">

        </div>
    </div>
    <div class="form-group">
        <label for="modifiers">personality modifiers:</label>
        <select class="form-control" id="modifiers">
            %for personality in personalities:
            <option value='{{personality["data"]["id"]}}'>{{personality["data"]["name"]}}</option>
            %end
        </select>
    </div>

    <div class="form-group">
        <input class="form-control" type="number" min="0" max="100" id="modifier" >
        <button onclick="addModifier()" class="btn btn-primary">+</button>
    </div>

    <div class="modifiers">

    </div>

    <div class="editNode">
        <button onclick="graphToXML()" class="btn btn-success">save</button>
        <button onclick="deleteNode()" class="btn btn-danger">delete</button>
    </div>
</div>

<h1>Graph</h1>
<div id="cy">
    <div style="padding: 5px; position: absolute; z-index: 2000;" class="newNode">
        <button onclick="newNode()"  class="btn btn-primary">New node +</button>
    </div>
</div>


</body>

<script>
    var nodes = {{!nodes}};
    var edges = {{!edges}};
    var personalities = {{!personalities}};

    var cy = cytoscape({
                container: $('#cy')[0],

                boxSelectionEnabled: false,
                autounselectify: true,

                style: cytoscape.stylesheet()
                        .selector('node')
                        .css({
                            'content': 'data(name)'
                        })
                        .selector('edge')
                        .css({
                            'target-arrow-shape': 'triangle',
                            'width': 4,
                            'line-color': '#ddd',
                            'target-arrow-color': '#ddd',
                            'curve-style': 'bezier'
                        })
                        .selector('.highlighted')
                        .css({
                            'background-color': '#61bffc',
                            'line-color': '#61bffc',
                            'target-arrow-color': '#61bffc',
                            'transition-property': 'background-color, line-color, target-arrow-color',
                            'transition-duration': '0.5s'
                        }),

                elements: {
                    nodes: nodes,
                    edges: edges
                },
                layout: {
                    name: 'grid',
                    padding: 10
                }
            })
            ;

    cy.on('tap', 'node', function (evt) {
        var node = evt.cyTarget;
        $("#nodeName").html(node.data('name'));
        $("#nodeId").html(node.data('id'));
        $("#actionName").val(node.data('name'));
        $("#animationName").val(node.data('animationname'));
        refreshModifiers();
        refreshNeighbours();
        refreshPositions();

    });

    function removeNeighbour(neighbourId, rootId) {
        for (var i = 0; i < edges.length; i++) {
            if (edges[i].data.source == rootId && edges[i].data.target == neighbourId) {
                var escapedEdgeId = edges[i].data.id;
                var edgeToRemove = cy.edges("edge[id='" + escapedEdgeId + "']");
                edgeToRemove.remove();
                break;
            }
        }

        refreshNeighbours()
    }

    function addNeighbour() {
        var nodeId = document.getElementById("nodeId").innerText;
        var neighbourId = document.getElementById("neighbours").value;

        cy.add([
            {group: "edges", data: {source: nodeId, target: neighbourId}}
        ]);

        refreshNeighbours();
    }

    function deleteNode() {
        var nodeId = document.getElementById("nodeId").innerText;
        var nodeToRemove = cy.nodes("node[id='" + nodeId + "']");
        nodeToRemove.remove();
    }

    function newNode() {
        var id = highestId() + 1;
        cy.add([
            {
                group: "nodes",
                data: {
                    id: id,
                    name: "newNode" + id,
                    animationname: "animation",
                    modifiers: [

                    ]
                    ,
                    position: {
                        x: 0,
                        y: 0,
                        z: 0
                    }
                },
                position:{x: cy.width()/2, y: cy.height()/2}
            }
        ]);
    }

    function refreshPositions() {
        var nodeId = document.getElementById("nodeId").innerText;
        var node = cy.nodes("node[id='" + nodeId + "']");
        $("#x").val(0);
        $("#y").val(0);
        $("#z").val(0);
        $("#x").val(node.data("position").x);
        $("#y").val(node.data("position").y);
        $("#z").val(node.data("position").z);
    }


    function refreshNeighbours() {
        var nodeId = document.getElementById("nodeId").innerText;
        var node = cy.nodes("node[id='" + nodeId + "']");
        $('.neighbours').empty();

        for (var i = 0; i < node.connectedEdges().length; i++) {
            var currentNodeName = node.connectedEdges()[i].target().data('name');

            if (currentNodeName != node.data('name'))
                $('.neighbours').append('<div class="col-md-8">' + currentNodeName + '' +
                        '</div> <div class="col-md-4">' +
                        '<button onclick="removeNeighbour(' + node.connectedEdges()[i].target().data("id") + ', ' + node.id() +
                        ')">-</button></div> ');
        }
    }

    function refreshModifiers() {
        var nodeId = document.getElementById("nodeId").innerText;
        var modifiers = cy.nodes("node[id='" + nodeId + "']").data("modifiers");
        $('.modifiers').empty();

        console.log(modifiers);
        for(var i = 0; i < modifiers.length; i++) {
            $('.modifiers').append('<div class="col-md-8">' + personalities[modifiers[i].id].data.name + ' ' + modifiers[i].value  +
                    '</div> <div class="col-md-4">' +
                    '<button onclick="removeModifier(' + modifiers[i].id  +
                    ')">-</button></div> ');
        }
    }

    function removeModifier(id) {
        var nodeId = document.getElementById("nodeId").innerText;
        var modifiers = cy.nodes("node[id='" + nodeId + "']").data("modifiers");

        for(var i = 0; i < modifiers.length; i++){
            if(modifiers[i].id == id){
                modifiers.splice(i, 1);
            }
        }
        cy.nodes("node[id='" + nodeId + "']").data("modifiers", modifiers);
        refreshModifiers();
    }

    function highestId() {

        var currentHeighest = 0;
        for(var i = 0; i < cy.nodes().length; i++) {
            currentHeighest = parseInt(cy.nodes()[i].data('id')) > currentHeighest ? parseInt(cy.nodes()[i].data('id')) : currentHeighest;
        }
        return parseInt(currentHeighest);
    }

    function addModifier() {
        var nodeId = document.getElementById("nodeId").innerText;
        var modifiers = cy.nodes("node[id='" + nodeId + "']").data("modifiers");
        var newModifier = {}
        var alreadyExists = false;

        for(var i = 0; i < modifiers.length; i++) {
            if(modifiers[i].id == $("#modifiers").val()){
                modifiers[i].value = $("#modifier").val();
                alreadyExists = true
            }
        }

        if(!alreadyExists) {
            newModifier["id"] = $("#modifiers").val();
            newModifier["value"] = $("#modifier").val();
            modifiers.push(newModifier)
        }
        cy.nodes("node[id='" + nodeId + "']").data("modifiers", modifiers);
        refreshModifiers();
    }

    function graphToXML() {
        var rootNode = {};
        var actionNode = {};

        rootNode["actions"] = [];
        actionNode["action"] = [];

        for(var i = 0; i < cy.nodes().length; i++) {
            var action = {};
            var neighbours = {};
            var modifiers = {};

            action["neighbours"] = [];
            action["modifiers"] = [];
            neighbours["neighbour"] = [];
            modifiers["modifier"] = [];


            for (var j = 0; j < cy.edges().length; j++) {
                if(cy.edges()[j].data('source') == cy.nodes()[i].data('id')){
                    neighbours["neighbour"].push(cy.edges()[j].data('target'));
                }
            }

            modifiers["modifier"].push(cy.nodes()[i].data("modifiers"));

            action["neighbours"].push(neighbours);
            action["modifiers"].push(modifiers);
            action["position"] = cy.nodes()[i].data("position")
            action["actionId"] = cy.nodes()[i].data('id');
            action["actionname"] = cy.nodes()[i].data('name');
            action["animationname"] = cy.nodes()[i].data('animationname');
            actionNode["action"].push(action);
        }
        rootNode["actions"].push(actionNode)

        $.ajax({
            type: 'POST',
            url: '/',
            data: {graph: json2xml(rootNode)}
        });
    }

    $('#actionName').on("change paste keyup", function() {
        var nodeId = document.getElementById("nodeId").innerText;
        cy.nodes("node[id='" + nodeId + "']").data("name", $("#actionName").val());
        $("#nodeName").text($("#actionName").val());
    });

    $('#animationName').on("change paste keyup", function() {
        var nodeId = document.getElementById("nodeId").innerText;
        cy.nodes("node[id='" + nodeId + "']").data("animationname", $("#animationName").val());
    });

    $('#x,#y,#z').on("change paste keyup", function() {
        var nodeId = document.getElementById("nodeId").innerText;
        var position = cy.nodes("node[id='" + nodeId + "']").data("position");

        position.x = $('#x').val();
        position.y = $('#y').val();
        position.z = $('#z').val();

        cy.nodes("node[id='" + nodeId + "']").data("position", position);
    });


</script>


</html>
