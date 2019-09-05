"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var ReactDOM = require("react-dom");
require("./styles/main.scss");
var App_1 = require("./components/App");
var TaskServiceDummy_1 = require( './services/TaskServiceDummy');
var TaskService_1 = require("./services/TaskService");
var root = document.getElementById('root');
var service = new TaskServiceDummy_1.TaskServiceDummy();
//var service = new TaskService_1.TaskService();
ReactDOM.render(React.createElement(App_1.App, { taskService: service }), root);
//# sourceMappingURL=index.js.map