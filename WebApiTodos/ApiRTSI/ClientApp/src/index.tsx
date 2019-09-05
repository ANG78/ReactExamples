import * as React from 'react';
import * as ReactDOM from 'react-dom';

import './styles/main.scss';

import { App } from './components/App';
import { TaskServiceDummy } from './services/TaskServiceDummy';
import { TaskService } from './services/TaskService';

const root = document.getElementById('root');
const service = new TaskServiceDummy();
//const service = new TaskService(); 

ReactDOM.render(<App taskService={service} />,root)