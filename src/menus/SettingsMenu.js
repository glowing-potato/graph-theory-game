import React from "react";
import { Link } from "react-router-dom";
import background from "./background.svg";
import "./SettingsMenu.css";

export default class SettingsMenu extends React.Component {
    render() {
        return (
            <div className="settings-menu">
                <h1>Settings</h1>
                <p>
                    TODO
                </p>
                <Link to="/">Back to Main Menu</Link>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
