import { useState, useEffect } from "react";
import {useNavigate, useParams} from "react-router-dom";
import {deletePost, getPostById} from "../../modules/postManager";
import {Button, Form, FormGroup, Label} from "reactstrap";
import { dateFormatter } from "../../Helpers/dateFormatter";

export const PostDelete = () => {
    const [post, setPost] = useState({
        title: "",
        createdDateTime: ""
    })

    const navigate = useNavigate()
    const {id} = useParams()

    const getPost = () => {
        getPostById(id)
        .then(post => setPost(post))
    }

    const handleClickDelete = () => {
        deletePost(id)
        .then(() => navigate("/posts"))
    }

    useEffect(() => {
        getPost()
    }, [])
    
    return (
        <Form>
            <FormGroup>
                <Label>Are you sure you'd like to delete the post titled: <b>{post.title}</b>? 
                        <br/>Originally posted on: <b>{dateFormatter(post.createdDateTime)}</b>
                </Label>
            </FormGroup>
            <FormGroup>
                <Button color="danger" onClick={() => handleClickDelete()}>Delete</Button>
                <Button onClick={() => navigate(-1)}>Cancel</Button>
            </FormGroup>
        </Form>
    )
}