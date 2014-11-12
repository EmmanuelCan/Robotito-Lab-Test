using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Runtime.Serialization.Json;
using RestJsonRobotitoLabContract;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response data;
            data = MakeRequest("http://json.robotcitolab.com");
            Session["response"] = data.users .ToList() ;

            GridView1.DataBind();


        }
    }
    public string GetMessage(string endPoint)
    {
        HttpWebRequest request = CreateWebRequest(endPoint);

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            var responseValue = string.Empty;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            // grab the response  
            using (var responseStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    responseValue = reader.ReadToEnd();
                }
            }

            return responseValue;
        }
    }

    private HttpWebRequest CreateWebRequest(string endPoint)
    {
        var request = (HttpWebRequest)WebRequest.Create(endPoint);

        request.Method = "GET";
        request.ContentLength = 0;
        request.ContentType = "text/xml";

        return request;
    }



    public static Response MakeRequest(string requestUrl)
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse res = request.GetResponse() as HttpWebResponse)
            {
                if (res.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    res.StatusCode,
                    res.StatusDescription));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                object objResponse = jsonSerializer.ReadObject(res.GetResponseStream());
                Response jsonResponse = objResponse as Response;
                return jsonResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }




    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();

    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        if (GridView1.DataSource == null)
        {
            List<RestJsonRobotitoLabContract.User> res = (List<RestJsonRobotitoLabContract.User>)Session["response"];

            GridView1.DataSource = res;
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (e.NewValues["UserName"] != null)
        {
            if (e.NewValues["UserName"] == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Debe Capturar el nombre')", true);
            }
                else{
            
            List<RestJsonRobotitoLabContract.User> res = (List<RestJsonRobotitoLabContract.User>)Session["response"];
            RestJsonRobotitoLabContract.User qry = (from p in res.AsEnumerable() where p.userName == Session["Name"].ToString() select p).SingleOrDefault();
            if (qry != null)
            {
                qry.userName = e.NewValues["UserName"].ToString();
                qry.email = e.NewValues["Email"].ToString();
                qry.phone = e.NewValues["Phone"].ToString();
                Session["response"] = res;
                GridView1.Rows[e.RowIndex].Cells[1].Text = e.NewValues["UserName"].ToString();
                GridView1.Rows[e.RowIndex].Cells[2].Text = e.NewValues["Email"].ToString();
                GridView1.Rows[e.RowIndex].Cells[3].Text = e.NewValues["Phone"].ToString();

            }

            btnNew.Visible = true;
            GridView1.EditIndex = -1;
            GridView1.DataBind();
                }
        }else{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Complete the information please')", true);
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Session["Name"] = GridView1.Rows[e.NewEditIndex].Cells[1].Text;
        GridView1.DataBind();

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

        GridView1.DataBind();
        btnNew.Visible = true;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       switch(e.CommandName )

        {
           case "New":
            List<RestJsonRobotitoLabContract.User> res = (List<RestJsonRobotitoLabContract.User>)Session["response"];
            RestJsonRobotitoLabContract.User u = new User();



            res.Add(u);


            Session["response"] = res;
            GridView1.DataBind();
               break ;
           case "Cancel":
               GridView1.EditIndex = -1;
               GridView1.DataBind();

               break;

        }
    

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        List<RestJsonRobotitoLabContract.User> res = (List<RestJsonRobotitoLabContract.User>)Session["response"];
        RestJsonRobotitoLabContract.User u = new User();

        Session["Name"] = "";
        u.userName = "";
        res.Insert(0, u);

        Session["response"] = res;
        GridView1.EditIndex = 0;
        GridView1.DataBind();
        btnNew.Visible = false;
        
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (GridView1.Rows[0].Cells[1].Text == "&nbsp;")
        {

            List<RestJsonRobotitoLabContract.User> res = (List<RestJsonRobotitoLabContract.User>)Session["response"];

            res.RemoveAt(0);
            
            Session["response"] = res;
            btnNew.Visible = true;
        }

        
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
}