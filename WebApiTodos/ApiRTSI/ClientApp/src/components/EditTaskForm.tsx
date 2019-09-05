import React, { Component } from 'react';
import { TaskImp } from '../model/TaskImp';
import { EnumStatusTask } from '../interfaces/EnumStatusTask';
import { ITask } from '../interfaces/ITask';

interface IProps {
  onAddTask: (task: ITask) => Promise<ITask>
  onCloseEdition: () => any;
}

interface IState {
  title: string,
  description: string,
  isLoading: boolean,
  messageError: string
}

export class EditTaskForm extends Component<IProps, IState> {

  status: EnumStatusTask = EnumStatusTask.Pending

  constructor(props: IProps) {
    super(props);
    this.state = {
      description: "",
      title: "",
      isLoading: false,
      messageError: ""
    };
  }

  handleChangeInputs(e: React.ChangeEvent<HTMLTextAreaElement>) {

    e.preventDefault();
    const { name, value } = e.target;

    if (name === "description") {
      this.setState({
        description: value
      })
    }

    if (name === "title") {
      this.setState({
        title: value
      })
    }
  }

  private handleError( mssErr  : string)
  {
    //alert(mssErr);
    this.setState({
      isLoading: false,
      messageError : mssErr
    });
  }

  handleCreate(e: React.FormEvent<HTMLFormElement>) {

    e.preventDefault();

    this.setState({
      isLoading: true,
      messageError: ""
    });

    var task = new TaskImp();
    task.title = this.state.title;
    task.description = this.state.description;

    this.props.onAddTask(task)
      .then((response) => {

        this.setState({
          title: "",
          description: "",
          isLoading: false,
          messageError : ""
        });

      })
      .catch((response: Response) => {

        if (response.text !== undefined) {
          response.text()
            .then((data: string) => {
              this.handleError(data);
            });
        }

      })
      .catch((response: string) => {
        this.handleError(response);
      });
  }

  render() {
    return (
      
      <div className="card editTaskForm">

        <form className="card-body" onSubmit={e => this.handleCreate(e)} >
          <div className="form-group">
            <label>Title:</label>
            <textarea
              name="title"
              className="form-control"
              placeholder="Title"
              onChange={e => this.handleChangeInputs(e)}
              value={this.state.title}
            ></textarea>
          </div>

          <div className="form-group">
            <label>Description:</label>
            <textarea
              className="form-control"
              name="description"
              placeholder="Description"
              onChange={e => this.handleChangeInputs(e)}
              value={this.state.description}
            ></textarea>
          </div>

          {this.state.messageError !== "" &&
            <div className="form-group">
              <label 
               className="badge bagde-pill badge-danger">Error:</label>
              <textarea
                className="form-control messageError"
                name="messageError"
                placeholder="messageError"
                value={this.state.messageError}
                disabled={true}
              ></textarea>

            </div>}

          <div className="container">
            <div className="row">
              <div className="col-3">
                <button
                  type="submit"
                  className="btn btn-primary"
                  name="Close"
                  onClick={e => this.props.onCloseEdition()}
                  value="Close">Close
              </button>
              </div>
              <div className="col-3">
                <button
                  type="submit"
                  className="btn btn-primary"
                  name="Create"
                  disabled={this.state.isLoading}
                  value="Create">Create
              </button>
              </div>
             
            </div>
          </div>
        </form>
      </div>

    );
  }
}
