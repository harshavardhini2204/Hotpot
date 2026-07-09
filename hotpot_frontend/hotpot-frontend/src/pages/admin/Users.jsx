import { useEffect, useState } from "react";
import userService from "../../services/userService";
import { toast } from "react-toastify";

function Users() {
    const [users, setUsers] = useState([]);
    const [search, setSearch] = useState("");

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await userService.getUsers();
                setUsers(data);
            } catch (error) {
                console.error(error);
                toast.error("Unable to load users");
            }
        };

        fetchUsers();
    }, []);

    const filteredUsers = users.filter(
        user =>
            user.fullName?.toLowerCase().includes(search.toLowerCase()) ||
            user.email?.toLowerCase().includes(search.toLowerCase())
    );

    return (
        <div className="admin-page">

            <div className="page-header">
                <h1>👥 User Management</h1>
                <p>Manage customers, admins and restaurant owners</p>
            </div>

            <div className="stats-row">
                <div className="stat-box">
                    <h2>{users.length}</h2>
                    <span>Total Users</span>
                </div>
            </div>

            <div className="search-container">
                <input
                    type="text"
                    className="search-box"
                    placeholder="Search users..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
            </div>

            <div className="table-modern mt-4">

                <table className="table mb-0">

                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Role</th>
                            <th>Status</th>
                        </tr>
                    </thead>

                    <tbody>

                        {filteredUsers.map(user => (

                            <tr key={user.userId}>

                                <td>#{user.userId}</td>

                                <td>
                                    <strong>
                                        {user.fullName}
                                    </strong>
                                </td>

                                <td>{user.email}</td>

                                <td>
                                    <span
                                        className={`role-badge ${
                                            user.role === "Admin"
                                                ? "role-admin"
                                                : user.role === "RestaurantOwner"
                                                ? "role-owner"
                                                : "role-user"
                                        }`}
                                    >
                                        {user.role}
                                    </span>
                                </td>

                                <td>
                                    <span
                                        className={
                                            user.isActive
                                                ? "status-active"
                                                : "status-inactive"
                                        }
                                    >
                                        {user.isActive
                                            ? "Active"
                                            : "Inactive"}
                                    </span>
                                </td>

                            </tr>

                        ))}

                    </tbody>

                </table>

            </div>

        </div>
    );
}

export default Users;