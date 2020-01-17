package com.pluralsight.module2;
import com.pluralsight.*;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import com.gargoylesoftware.htmlunit.ElementNotFoundException;
import com.gargoylesoftware.htmlunit.WebClient;
import com.gargoylesoftware.htmlunit.WebResponse;
import com.gargoylesoftware.htmlunit.html.DomElement;
import com.gargoylesoftware.htmlunit.html.DomNodeList;
import com.gargoylesoftware.htmlunit.html.HtmlPage;
import com.gargoylesoftware.htmlunit.html.HtmlAnchor;
import com.gargoylesoftware.htmlunit.html.HtmlForm;
import com.gargoylesoftware.htmlunit.html.HtmlInput;

import static org.junit.Assert.*;

import java.io.IOException;

public class Module2_Task7_thru10_IT {
	private String BOOK_FORM_NAME = "book_form";
	private String indexUrl;
	private WebClient webClient;
  HtmlPage firstPage;
	HtmlPage editPage;
	HtmlPage newPage;

	  @Before
	  public void setUp() throws IOException {
	    indexUrl = "http://localhost:8080"; //System.getProperty("integration.base.url");
	    webClient = new WebClient();
			// Open the admin page
	    firstPage = webClient.getPage(indexUrl + "/books/admin");

      try {
        for (  HtmlAnchor a : firstPage.getAnchors()) {
          String href = a.getHrefAttribute();
          if (href.contains("edit")) {
            editPage = a.click();
          }
					else if (href.contains("new")) {
            newPage = a.click();
          }
        }
      }
      catch (  Exception e) {}
	  }

	  @After
	  public void tearDown() {
	    webClient.closeAllWindows();
	  }

		// Verify they adapted the BookForm.jsp page for editing existing books
		// and adding new book
		// In this test check the form action is conditional, and the form h2
    @Test
	  public void _task7() {
      assertNotNull("Link, edit, did not work.", editPage);
			checkForm("Edit");
    }

		@Test
	  public void _task8() {
			assertNotNull("Link, edit, did not work.", editPage);
			checkForm("Edit");
      assertNotNull("Link, new, did not work.", newPage);
			checkForm("New");
    }

		@Test
	  public void _task9() {
			assertNotNull("Link, edit, did not work.", editPage);
			h2_correct("Edit");
    }

		@Test
	  public void _task10() {
			assertNotNull("Link, edit, did not work.", editPage);
			h2_correct("Edit");
			assertNotNull("Link, new, did not work.", newPage);
			h2_correct("New");
    }

	  public void h2_correct(String urlStr) {
      // First check if an H2 exists with text "New Book Form"
      boolean h2Text_correct = false;
      DomNodeList< DomElement > list;
			if (urlStr.equals("Edit")) list = editPage.getElementsByTagName( "h2" );
			else list = newPage.getElementsByTagName( "h2" );
			String h2Text = "";
			String desiredText = urlStr + " Book Form";
			desiredText = desiredText.replaceAll("\\s+","");
			for( DomElement domElement : list )
      {
					h2Text = domElement.getTextContent();
					h2Text = h2Text.replaceAll("\\s+","");
          if (h2Text.equals(desiredText))
            h2Text_correct = true;
      }
			String errorMsg = "The h2 tag in BookForm contains "+ h2Text +
												" but we expected it to contain " + desiredText;
      assertTrue(errorMsg, h2Text_correct);
	  }

		public void checkForm(String urlStr) {
			// Get form and check action
			HtmlForm form = null;
			String errorMsg = "";
			String desiredAction = "";
			try {
				if (urlStr.equals("Edit")) {
					form = editPage.getFormByName(BOOK_FORM_NAME);
					errorMsg = "Form, book_form, action not \"update\".";
					desiredAction = "update";
				}
				else {
					form = newPage.getFormByName(BOOK_FORM_NAME);
					errorMsg = "Form, book_form, action not \"insert\".";
					desiredAction = "insert";
				}
			} catch (ElementNotFoundException e) {}

			String formErrorMsg = "We can’t find a <form> with name 'book_form' in BookForm.jsp";
			assertNotNull(formErrorMsg, form);
			String action = form.getActionAttribute();
			assertEquals(errorMsg, desiredAction, action);
		}
}
