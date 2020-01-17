<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<%@ taglib uri = "http://java.sun.com/jsp/jstl/core" prefix = "c" %>
<%@ taglib prefix = "fmt" uri = "http://java.sun.com/jsp/jstl/fmt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>Book Store</title>
    <link rel="stylesheet" type="text/css" href="${pageContext.request.contextPath}/css/style.css">
</head>

<body>
	<ul>
	  <li><a href="/books/list">Book Listing</a></li>
    <li><a href="/books/admin">Admin</a></li>
    <li><a class="active" href="/cart/">Cart</a></li>
	</ul>

    <div class="container">
	    <div class="booktable">
	        <table border="1" cellpadding="5">
	            <caption>List of Books</caption>
	            <tr>
	                <th>Title</th>
	                <th>Author</th>
	                <th>Price</th>
                  <th>Quantity</th>
                  <th>Total Cost</th>
                  <th></th>
	            </tr>

      	 			<c:forEach items="${cart.cartItems}" var="cartItem">
                  <tr>
                      <td> ${ cartItem.getTitle() } </td>
                      <td> ${ cartItem.getAuthor() } </td>
                      <td> <fmt:formatNumber value = "${ cartItem.getPrice() }" type = "currency"/>  </td>
                      <td>${ cartItem.getQuantity() }</td>
                      <td> <fmt:formatNumber value = "${ cartItem.getTotalCost() }" type = "currency"/> </td>
                      <td>Update Delete</td>
                  </tr>
              </c:forEach>
	        </table>
	    </div>
    </div>
</body>
</html>
