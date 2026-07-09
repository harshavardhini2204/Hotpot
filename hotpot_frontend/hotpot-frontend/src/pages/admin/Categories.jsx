import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import categoryService from "../../services/categoryService";

function Categories() {

    const [categories, setCategories] = useState([]);

    const [categoryName, setCategoryName] = useState("");

    const [description, setDescription] = useState("");

    const [editingId, setEditingId] = useState(null);
    const loadCategories = async () => {

    try {

        const data = await categoryService.getAllCategories();

        setCategories(data);

    }

    catch (error) {

        console.error(error);

    }

};

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

   

    const handleSubmit = async (e) => {

        e.preventDefault();

        const category = {

            categoryName,

            description

        };

        try {

            if (editingId === null) {

                await categoryService.createCategory(category);

                toast.success("Category Added");

            }

            else {

                await categoryService.updateCategory(editingId, category);

                toast.success("Category Updated");

            }

            setCategoryName("");

            setDescription("");

            setEditingId(null);

            loadCategories();

        }

        catch (error) {

            console.error(error);

            toast.error("Operation Failed");

        }

    };

    const handleEdit = (category) => {

        setEditingId(category.categoryId);

        setCategoryName(category.categoryName);

        setDescription(category.description);

    };

    const handleDelete = async (id) => {

        if (!window.confirm("Delete this category?"))
            return;

        try {

            await categoryService.deleteCategory(id);

            toast.success("Category Deleted");

            loadCategories();

        }

        catch (error) {

            console.error(error);

            toast.error("Delete Failed");

        }

    };

    return (
    <div className="admin-page">

        <div className="admin-header">
            <h1>📂 Category Management</h1>
            <p>Create, edit and organize food categories</p>
        </div>

        <div className="container-fluid py-4">

            <div className="row g-4">

                {/* FORM */}
                <div className="col-lg-4">

                    <div className="admin-card">

                        <h4 className="mb-4">
                            {editingId
                                ? "✏️ Edit Category"
                                : "➕ Add Category"}
                        </h4>

                        <form onSubmit={handleSubmit}>

                            <input
                                className="form-control modern-input mb-3"
                                placeholder="Category Name"
                                value={categoryName}
                                onChange={(e) =>
                                    setCategoryName(e.target.value)
                                }
                            />

                            <textarea
                                className="form-control modern-input mb-3"
                                rows="4"
                                placeholder="Description"
                                value={description}
                                onChange={(e) =>
                                    setDescription(e.target.value)
                                }
                            />

                            <button
                                type="submit"
                                className="admin-btn w-100"
                            >
                                {editingId
                                    ? "Update Category"
                                    : "Add Category"}
                            </button>

                        </form>

                    </div>

                </div>

                {/* TABLE */}
                <div className="col-lg-8">

                    <div className="admin-card">

                        <div className="d-flex justify-content-between align-items-center mb-4">

                            <h4 className="mb-0">
                                Categories
                            </h4>

                            <span className="badge bg-primary fs-6">
                                {categories.length} Categories
                            </span>

                        </div>

                        <div className="table-responsive">

                            <table className="table modern-table align-middle">

                                <thead>

                                    <tr>
                                        <th>ID</th>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th>Actions</th>
                                    </tr>

                                </thead>

                                <tbody>

                                    {categories.map(category => (

                                        <tr key={category.categoryId}>

                                            <td>
                                                #{category.categoryId}
                                            </td>

                                            <td>
                                                <strong>
                                                    {category.categoryName}
                                                </strong>
                                            </td>

                                            <td>
                                                {category.description}
                                            </td>

                                            <td>

                                                <button
                                                    className="btn btn-sm btn-warning me-2"
                                                    onClick={() =>
                                                        handleEdit(category)
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                <button
                                                    className="btn btn-sm btn-danger"
                                                    onClick={() =>
                                                        handleDelete(
                                                            category.categoryId
                                                        )
                                                    }
                                                >
                                                    Delete
                                                </button>

                                            </td>

                                        </tr>

                                    ))}

                                </tbody>

                            </table>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </div>
);

}

export default Categories;