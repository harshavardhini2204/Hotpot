import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import categoryService from "../../services/categoryService";
import menuItemService from "../../services/menuService";

function AddMenuItem() {

    const navigate = useNavigate();

    const restaurantId = Number(localStorage.getItem("restaurantId"));

    const [categories, setCategories] = useState([]);

    const [menuItem, setMenuItem] = useState({

        restaurantId,

        categoryId: "",

        itemName: "",

        description: "",

        ingridients: "",

        price: "",

        discountPrice: "",

        cookingTime: "",

        availabilityTime: "",

        dietaryType: "",

        tasteInfo: "",

        calories: "",

        protein: "",

        carbs: "",

        fat: "",

        imageUrl: "",

        isAvailable: true

    });

    useEffect(() => {

        const fetchCategories = async () => {

            try {

                const data = await categoryService.getAllCategories();

                setCategories(data);

            }

            catch (error) {

                console.error(error);

            }

        };

        fetchCategories();

    }, []);

    const handleChange = (e) => {

        const { name, value, type, checked } = e.target;

        setMenuItem({

            ...menuItem,

            [name]: type === "checkbox" ? checked : value

        });

    };

    const handleSubmit = async (e) => {

        e.preventDefault();

        try {

            await menuItemService.createMenuItem(menuItem);

            toast.success("Menu Item Added Successfully");

            navigate("/owner/menuitems");

        }

        catch (error) {

            console.error(error);

    console.log(error.response?.data);

    toast.error("Unable to Add Menu Item");

        }

    };

    return (

        <div className="menu-page">

    <div className="menu-banner">
        <h1>Add New Menu Item</h1>
        <p>Create delicious dishes for your customers</p>
    </div>

    <div className="container py-5">

        <form onSubmit={handleSubmit}>

            <div className="row g-4">

                {/* LEFT SIDE */}
                <div className="col-lg-8">

                    <div className="form-card">

                        <h4>Basic Information</h4>

                        <input
                            className="form-control modern-input mb-3"
                            name="itemName"
                            placeholder="Item Name"
                            value={menuItem.itemName}
                            onChange={handleChange}
                        />

                        <textarea
                            className="form-control modern-input mb-3"
                            rows="4"
                            name="description"
                            placeholder="Description"
                            value={menuItem.description}
                            onChange={handleChange}
                        />

                        <textarea
                            className="form-control modern-input"
                            rows="3"
                            name="ingridients"
                            placeholder="Ingredients"
                            value={menuItem.ingridients}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-card mt-4">

                        <h4>Category & Pricing</h4>

                        <select
                            className="form-select modern-input mb-3"
                            name="categoryId"
                            value={menuItem.categoryId}
                            onChange={handleChange}
                        >
                            <option value="">
                                Select Category
                            </option>

                            {categories.map(category => (

                                <option
                                    key={category.categoryId}
                                    value={category.categoryId}
                                >
                                    {category.categoryName}
                                </option>

                            ))}
                        </select>

                        <div className="row">

                            <div className="col-md-6">
                                <input
                                    className="form-control modern-input mb-3"
                                    name="price"
                                    type="number"
                                    placeholder="Price"
                                    value={menuItem.price}
                                    onChange={handleChange}
                                />
                            </div>

                            <div className="col-md-6">
                                <input
                                    className="form-control modern-input mb-3"
                                    name="discountPrice"
                                    type="number"
                                    placeholder="Discount Price"
                                    value={menuItem.discountPrice}
                                    onChange={handleChange}
                                />
                            </div>

                        </div>

                    </div>

                    <div className="form-card mt-4">

                        <h4>Food Details</h4>

                        <div className="row">

                            <div className="col-md-6">
                                <input
                                    className="form-control modern-input mb-3"
                                    name="cookingTime"
                                    placeholder="Cooking Time"
                                    value={menuItem.cookingTime}
                                    onChange={handleChange}
                                />
                            </div>

                            <div className="col-md-6">
                                <input
                                    className="form-control modern-input mb-3"
                                    name="availabilityTime"
                                    placeholder="Availability Time"
                                    value={menuItem.availabilityTime}
                                    onChange={handleChange}
                                />
                            </div>

                        </div>

                        <input
                            className="form-control modern-input mb-3"
                            name="dietaryType"
                            placeholder="Dietary Type"
                            value={menuItem.dietaryType}
                            onChange={handleChange}
                        />

                        <input
                            className="form-control modern-input"
                            name="tasteInfo"
                            placeholder="Taste Info"
                            value={menuItem.tasteInfo}
                            onChange={handleChange}
                        />

                    </div>

                </div>

                {/* RIGHT SIDE */}
                <div className="col-lg-4">

                    <div className="preview-card">

                        <h4>Image Preview</h4>

                        <img
                            src={
                                menuItem.imageUrl ||
                                "https://via.placeholder.com/350x220?text=Food+Preview"
                            }
                            alt="preview"
                            className="preview-image"
                        />

                        <input
                            className="form-control modern-input mt-3"
                            name="imageUrl"
                            placeholder="Image URL"
                            value={menuItem.imageUrl}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-card mt-4">

                        <h4>Nutrition</h4>

                        <input
                            className="form-control modern-input mb-2"
                            name="calories"
                            placeholder="Calories"
                            value={menuItem.calories}
                            onChange={handleChange}
                        />

                        <input
                            className="form-control modern-input mb-2"
                            name="protein"
                            placeholder="Protein"
                            value={menuItem.protein}
                            onChange={handleChange}
                        />

                        <input
                            className="form-control modern-input mb-2"
                            name="carbs"
                            placeholder="Carbs"
                            value={menuItem.carbs}
                            onChange={handleChange}
                        />

                        <input
                            className="form-control modern-input"
                            name="fat"
                            placeholder="Fat"
                            value={menuItem.fat}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-card mt-4">

                        <div className="form-check">

                            <input
                                type="checkbox"
                                className="form-check-input"
                                name="isAvailable"
                                checked={menuItem.isAvailable}
                                onChange={handleChange}
                            />

                            <label className="form-check-label">
                                Available for Ordering
                            </label>

                        </div>

                        <button
                            className="save-menu-btn mt-4"
                        >
                            Add Menu Item
                        </button>

                    </div>

                </div>

            </div>

        </form>

    </div>

</div>

    );

}

export default AddMenuItem;