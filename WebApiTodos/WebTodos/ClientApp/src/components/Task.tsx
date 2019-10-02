import React, { Component } from 'react';
import { ITask } from "../interfaces/ITask";
import { EnumStatusTask } from "../interfaces/EnumStatusTask";


interface IProps {
  task: ITask
  onToggleStatus: (task: ITask) => Promise<ITask>
}

interface IState {
  current: ITask,
  action: string,
  hideDescription: boolean
}

export class Task extends Component<IProps, IState> {

  constructor(props: IProps) {
    super(props);

    var action = "Complete";
    if (props.task.status === EnumStatusTask.Completed) {
      action = "Pending";
    }

    this.state = {
      current: props.task,
      action: action,
      hideDescription: false,
    }

    this.hanlerToggleAdapter = this.hanlerToggleAdapter.bind(this);
    this.hideMyDecription = this.hideMyDecription.bind(this);
  }

  hanlerToggleAdapter(e: any) {

    e.preventDefault();
    if (this.props.onToggleStatus !== undefined) {
      this.props.onToggleStatus(this.state.current)
        .then((response) => { })
        .catch((response) => {

          response.text()
            .then((data: string) => {
              alert('it is not possible to change the status.' + data);
            });
        })
    }
  }

  hideMyDecription() {
    this.setState({
      hideDescription: !this.state.hideDescription
    });
  }

  handlerOnDraggStart(ev:React.DragEvent<HTMLDivElement>): void
  {
    const id = this.state.current.id;
    ev.dataTransfer.setData("text/plain", "" + id);
  }


  render() {
    return (
      <div key={this.state.current.id}
        className="draggable task"
      
        draggable
        onDragStart={(e) => { this.handlerOnDraggStart(e)}}
        >     
        <div className="card mt-2 ml-2">
          <div className="card-body" >
            <div className="form-group">
              <label
                className={"badge bagde-pill " +
                  (this.state.current.status === EnumStatusTask.Completed ?
                    'badge-danger' : 'badge-success')}
              >
                {this.state.current.status[0]}
              </label>
              <label
                className="ml-1 badge badge-primary"
                onClick={(e) => this.hideMyDecription()}>TITLE   :</label>
              <textarea
                className="form-control ml-10"
                readOnly={true}
                value={this.state.current.title}></textarea>
            </div>


            {!this.state.hideDescription &&
              <div className="form-group">
                <label className="badge badge-info">DESCRIPTION   :</label>
                <textarea
                  className="form-control ml-10"
                  readOnly={true}
                  value={this.state.current.description}
                ></textarea>
              </div>
            }

            <button
              className="btn btn-success"
              name="Create"
              value="Complete"
              onClick={(e) => this.hanlerToggleAdapter(e)}
            >{this.state.action}</button>
          </div>
        </div>
      </div>
    );
  }
}
