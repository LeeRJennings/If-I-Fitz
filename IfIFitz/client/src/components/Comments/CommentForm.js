import { useState, useEffect } from "react"
import { addComment } from "../../modules/commentManager";
import { useNavigate, useParams } from "react-router-dom"
import { Button,Form,FormGroup,Input,Label } from 'reactstrap';


export const CommentForm = () => {
    const [comment, setComment] = useState({
        content: ""
    })
    const [isLoading, setIsLoading] = useState(true)

    const navigate = useNavigate()
    const {id} = useParams()

    const handleFieldChange = (e) => {
        const newComment = {...comment}
        let selectedVal = e.target.value
        newComment[e.target.id] = selectedVal
        setComment(newComment)
    }

    const handleClickSave = () => {
        if (comment.content === "") {
            window.alert("A comment with no content seems pretty pointless ........")
        } else {
            setIsLoading(true)
            comment.postId = id
            addComment(comment)
            .then(() => navigate(`/posts/${id}`))
        }
    }

    useEffect(() => {
        setIsLoading(false)
    }, [])

    return (
        <Form className="m-2 noBorderForm">
            <h2>Add a New Comment </h2>
            <FormGroup>
                <Input type="textarea" 
                        name="content" 
                        id="content"
                        onChange={handleFieldChange}
                        value={comment.content}
                        placeholder="Write your comment here..." />
            </FormGroup>
            <Button color="success" onClick={() => handleClickSave()} disabled={isLoading}>Add Comment</Button>
            <Button className="m-2" onClick={() => navigate(-1)}>Cancel</Button>
        </Form>
    )
}