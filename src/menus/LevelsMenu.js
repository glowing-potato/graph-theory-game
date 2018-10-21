import React from "react";
import { Link } from "react-router-dom";
import background from "./background.svg";
import "./LevelsMenu.css";

const levels = [
    { "id": "euler-trail", "name": "Euler Trails" },
    { "id": "hamilton-path", "name": "Hamiltonian Paths" },
    { "id": "euler-cycle", "name": "Euler Cycles" },
    { "id": "hamilton-circuit", "name": "Hamiltonian Circuits" }
];

export default class LevelsMenu extends React.Component {
    render() {
        return (
            <div className="levels-menu">
                <h1>Select Game Mode</h1>
                <ul>
                    {levels.map((level, i) => (
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
