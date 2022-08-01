import { useState, useEffect, useCallback } from "react"
import { updatePost, getPostById } from "../../modules/postManager"
import { getAllMaterials } from "../../modules/materialManager"
import { getAllSizes } from "../../modules/sizeManager"
import { useNavigate, useParams } from "react-router-dom"
import { Button,Form,FormGroup,Input,Label } from 'reactstrap';


export const PostEdit = () => {
    const [isLoading, setIsLoading] = useState(true)
    const [materials, setMaterials] = useState([])
    const [sizes, setSizes] = useState([])
    const [post, setPost] = useState({
        title: "",
        imageLocation: "",
        sizeId: 0,
        materialId: 0,
        description: ""
    })

    const navigate = useNavigate()
    const {id} = useParams()

    const getPost = () => {
        getPostById(id)
        .then(post => {
            setPost(post)
        })
    }

    const handleFieldChange = (e) => {
        const newPost = {...post}
        let selectedVal = e.target.value
        newPost[e.target.id] = selectedVal
        setPost(newPost)
    }

    const handleClickSave = () => {
        if (post.title === "" || post.sizeId === 0 || post.materialId === 0 || post.description === "") {
            window.alert("All fields except Image URL are required")
        } else {
            setIsLoading(true)
            const newPost = {...post}
            delete newPost.userProfile
            updatePost(newPost)
            .then(() => navigate("/posts"))
        }
    }

    useEffect(() => {
        getPost()
        getAllMaterials()
        .then(m => {
            setMaterials(m)
        })
        getAllSizes()
        .then(s => {
            setSizes(s)
        })
        setIsLoading(false)
    }, [])
    
    return (
        <Form className="m-2 p-2">
            <h2>Edit Your Post</h2>
            <FormGroup>
                <Label for="title">Title:</Label>
                <Input type="text" 
                        name="title" 
                        id="title"
                        onChange={handleFieldChange}
                        value={post.title}
                        placeholder="Post Title" />
            </FormGroup>
            <FormGroup>
                <Label for="imageLocation">Image URL:</Label>
                <Input type="text" 
                        name="imageLocation" 
                        id="imageLocation"
                        onChange={handleFieldChange}
                        value={post.imageLocation}
                        placeholder="www.website.com" />
            </FormGroup>
            <FormGroup>
                <Label for="size">Size:</Label><br/>
                <select value={post.sizeId} name="size" id="sizeId" form="categoryForm" onChange={handleFieldChange}>
                <option hidden disabled value="0">--Select a Size--</option>
                {sizes.map(s => (
                    <option key={s.id} value={s.id}>
                    {s.name}
                    </option>
                ))}
                </select>
            </FormGroup>
            <FormGroup>
                <Label for="material">Material:</Label><br/>
                <select value={post.materialId} name="material" id="materialId" form="categoryForm" onChange={handleFieldChange}>
                <option hidden disabled value="0">--Select a Material--</option>
                {materials.map(m => (
                    <option key={m.id} value={m.id}>
                    {m.type}
                    </option>
                ))}
                </select>
            </FormGroup>
            <FormGroup>
                <Label for="description">Description:</Label>
                <Input type="textarea" 
                        name="description" 
                        id="description"
                        onChange={handleFieldChange}
                        value={post.description}
                        placeholder="Describe your post here..." />
            </FormGroup>
            <Button color="primary" onClick={() => handleClickSave()} disabled={isLoading}>Save Edits</Button>
            <Button className="ms-2" onClick={() => navigate(-1)}>Cancel</Button>
        </Form>
    )
}