import React from "react";
import { Link } from "react-router-dom";
import LevelData from "./LevelData";
import background from "./background.svg";
import "./LevelMenu.css";

export default class LevelMenu extends React.Component {
    render() {
        let levels = [];
        const data = LevelData.find(d => d.id === this.props.match.params.type);
        for (let i = 1; i <= data.levels; ++i) {
            levels.push((
                <li key={i}>
                    <Link to={`/${this.props.match.params.type}/${i}`}>{i}</Link>
                </li>
            ))
        }
        return (
            <div className="level-menu">
                <h1>Select Level</h1>
                <main>
                    <div className="padding" />
                    <ul>
                        {levels}
                    </ul>
                    <div className="padding" />
                </main>
                <Link to="/">Back to Main Menu</Link>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
