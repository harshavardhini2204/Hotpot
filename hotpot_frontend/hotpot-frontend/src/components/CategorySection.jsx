import { useEffect, useState } from "react";
import categoryService from "../services/categoryService";

function CategorySection() {

    const [categories, setCategories] = useState([]);

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

    return (

        <div className="container mt-5">

            <h2 className="text-center mb-4">
                Food Categories
            </h2>

            <div className="row">

                {categories.map(category => (

                    <div
                        key={category.categoryId}
                        className="col-md-3 mb-3"
                    >

                        <div className="card shadow text-center p-4">
               <img
    src={category.imageUrl}
    alt={category.categoryName}
    className="card-img-top"
    style={{
        height: "180px",
        width: "100%",
        objectFit: "cover"
    }}
/>

                            <h5>{category.categoryName}</h5>

                            <p>{category.description}</p>

                        </div>

                    </div>

                ))}

            </div>

        </div>

    );

}

export default CategorySection;