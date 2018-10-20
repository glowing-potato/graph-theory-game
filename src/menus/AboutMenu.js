import React from "react";
import { Link } from "react-router-dom";
import background from "./background.svg";
import "./AboutMenu.css";

export default class AboutMenu extends React.Component {
    render() {
        return (
            <div className="about-menu">
                <h1>About</h1>
                <p>
                    This is a game that was developed for the Hack K-State 2018, which is a MLH Hackathon that Kansas State Univeristy hosts.
                    The purpose of this game is to teach young people about fundamentals of Graph Theory in a fun, interactive way.
                </p>
                <Link to="/">Back to Main Menu</Link>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
