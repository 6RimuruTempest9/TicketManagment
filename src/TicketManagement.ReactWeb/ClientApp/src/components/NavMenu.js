import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { getJwt } from '../Helpers';
import Cookies from 'universal-cookie';
import { useHistory } from 'react-router-dom';
import { AppConfiguration } from "read-appsettings-json";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      isAuth: false,
    };
  }

  componentDidMount() {
    this.timerID = setInterval(async () => {
      const jwt = getJwt();

      const formData = new FormData();

      formData.append("jwt", jwt);

      const response = await fetch(AppConfiguration.Setting().CheckJwt, {
        method: "POST",
        body: formData,
      })

      if (response.ok) {
        this.setState({
          isAuth: true,
        });
      } else {
        this.setState({
          isAuth: false,
        });
      }
    }, 1000);
  }

  componentWillUnmount() {
    clearInterval(this.timerID);
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    let collapseElement;

    if (this.state.isAuth) {
      collapseElement = (
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/events/getall">Events</NavLink>
            </NavItem>
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/user/profile">Profile</NavLink>
            </NavItem>
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/user/history">Purchase history</NavLink>
            </NavItem>
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/" onClick={() => {
                const cookies = new Cookies();
                cookies.remove("JWT");
                this.setState({ isAuth: false });
              }}>Logout</NavLink>
            </NavItem>
          </ul>
        </Collapse>
      );
    } else {
      collapseElement = (
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/events/getall">Events</NavLink>
            </NavItem>
            <NavItem>
              <NavLink  tag={Link} className="text-dark" to="/authorization/login">Login</NavLink>
            </NavItem>
              <NavLink  tag={Link} className="text-dark" to="/authorization/register">Registration</NavLink>
            <NavItem>
            </NavItem>
          </ul>
        </Collapse>
      );
    }

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">TicketManagement.ReactWeb</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            {collapseElement}
          </Container>
        </Navbar>
      </header>
    );
  }
}