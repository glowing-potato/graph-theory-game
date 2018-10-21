import React from "react";
import { Link } from "react-router-dom";
import LevelData from "./LevelData";
import background from "./background.svg";
import "./LevelsMenu.css";

export default class LevelsMenu extends React.Component {
    render() {
        return (
            <div className="levels-menu">
                <h1>Select Game Mode</h1>
                <ul>
                    {LevelData.map((level, i) => (
                        <li key={i}>
                            <Link to={`/${level.id}/`}>{level.name}</Link>
                        </li>
                    ))}
                </ul>
                <Link to="/">Back to Main Menu</Link>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
