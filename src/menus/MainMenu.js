import React from "react";
import { Link } from "react-router-dom";
import background from "./background.svg";
import "./MainMenu.css";

export default class MainMenu extends React.Component {
    render() {
        return (
            <div className="main-menu">
                <h1>Blep-Blap</h1>
                <div className="menu">
                    <div className="padding" />
                    <ul>
                        <li>
                            <Link to="/levels">Play</Link>
                        </li>
                        <li>
                            <Link to="/about">About</Link>
                        </li>
                        {/*
                        <li>
                            <Link to="/settings">Settings</Link>
                        </li>
                        */}
                    </ul>
                    <div className="padding" />
                </div>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
