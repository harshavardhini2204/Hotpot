import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import menuItemService from "../../services/menuService";

function MenuManagement() {

    const restaurantId = Number(localStorage.getItem("restaurantId"));

    const [menuItems, setMenuItems] = useState([]);

    const [search, setSearch] = useState("");

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const fetchMenuItems = async () => {

            try {

                const data =
                    await menuItemService.getMenuItemsByRestaurant(restaurantId);

                setMenuItems(data);

            }

            catch (error) {

                console.error(error);

                toast.error("Unable to load menu items");

            }

            finally {

                setLoading(false);

            }

        };

        fetchMenuItems();

    }, [restaurantId]);

    const handleDelete = async (id) => {

        if (!window.confirm("Delete this menu item?"))
            return;

        try {

            await menuItemService.deleteMenuItem(id);

            toast.success("Menu Item Deleted");

            setMenuItems(menuItems.filter(item => item.menuItemId !== id));

        }

        catch (error) {

            console.error(error);

            toast.error("Delete Failed");

        }

    };

    const filteredItems = menuItems.filter(item =>
        item.itemName.toLowerCase().includes(search.toLowerCase())
    );

    if (loading)
        return <h3 className="text-center mt-5">Loading...</h3>;

   return (

    <div className="menu-page">

        <div className="menu-banner">

            <h1>🍔 Menu Management</h1>

            <p>
                Manage all menu items in your restaurant
            </p>

        </div>

        <div className="container py-4">

            <div className="menu-toolbar">

                <input

                    className="form-control menu-search"

                    placeholder="Search menu item..."

                    value={search}

                    onChange={(e) =>
                        setSearch(e.target.value)
                    }

                />

                <Link
                    className="add-menu-btn"
                    to="/owner/menuitems/add"
                >
                    ➕ Add Menu Item
                </Link>

            </div>

            <div className="row g-4">

                {filteredItems.map(item => (

                    <div
                        className="col-lg-4 col-md-6"
                        key={item.menuItemId}
                    >

                        <div className="menu-card">

                            <img
                                src={item.imageUrl}
                                alt={item.itemName}
                                className="menu-card-image"
                            />

                            <div className="menu-card-body">

                                <h4>
                                    {item.itemName}
                                </h4>

                                <div className="menu-price">
                                    ₹{item.price}
                                </div>

                                <div className="mb-3">

                                    {item.isAvailable ? (

                                        <span className="badge bg-success">
                                            Available
                                        </span>

                                    ) : (

                                        <span className="badge bg-danger">
                                            Not Available
                                        </span>

                                    )}

                                </div>

                                <div className="menu-actions">

                                    <Link
                                        className="btn btn-warning"
                                        to={`/owner/menuitems/edit/${item.menuItemId}`}
                                    >
                                        Edit
                                    </Link>

                                    <button
                                        className="btn btn-danger"
                                        onClick={() =>
                                            handleDelete(item.menuItemId)
                                        }
                                    >
                                        Delete
                                    </button>

                                </div>

                            </div>

                        </div>

                    </div>

                ))}

            </div>

        </div>

    </div>

);

}

export default MenuManagement;