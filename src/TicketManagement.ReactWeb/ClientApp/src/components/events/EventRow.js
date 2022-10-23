import React from "react";
import Cookies from "universal-cookie";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { getJwt } from "../../Helpers";
import { AppConfiguration } from "read-appsettings-json";

export class EventRow extends React.Component {
    constructor(props) {
        super(props);

        this.handleDeleteLinkClick = this.handleDeleteLinkClick.bind(this);

        this.state = {
            areas: [],
        }
    }

    async componentDidMount() {
        const response = await fetch(AppConfiguration.Setting().GetEventsByIdUrl + this.props.event.id);

        this.setState({
            areas: await response.json(),
        });
    }

    async handleDeleteLinkClick() {
        const jwt = getJwt();
        
        fetch(AppConfiguration.Setting().DeleteEventByIdUrl + this.props.event.id, {
            headers: {
                "Authorization": "Bearer " + jwt,
            }
        });
    }

    render() {
        let actionsTableBodyElement;

        if (this.props.isEditable) {
            actionsTableBodyElement = (
                <td>
                    <Link to={{ pathname: "/events/update", event: this.props.event }}>Edit</Link>
                    <br />
                    <a href="#" onClick={this.handleDeleteLinkClick}>Delete</a>
                </td>
            );
        }

        return (
            <tr>
                <td>{this.props.event.name}</td>
                <td>{this.props.event.description}</td>
                <td>{this.props.event.startDate.toLocaleString()}</td>
                <td>{this.props.event.endDate.toLocaleString()}</td>
                <td>
                    <img
                        alt="No image"
                        src={this.props.event.imageUrl}
                        style={{
                            maxHeight: "200px",
                            maxWidth: "200px",
                        }}
                    />
                </td>
                <td>
                    <Link to={{ pathname: "/eventareas/getallbyeventid", areas: this.state.areas }}>More info</Link>
                </td>
                {actionsTableBodyElement}
            </tr>
        );
    }
}