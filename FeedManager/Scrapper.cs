using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedManager
{
    public class Scrapper
    {
        public string scrapper()
        {
            string url = "http://www.frontera.info/rosarito";
            string tagContainer = "div";
            string attributeContainer = "id";
            string attributeContainerValue = "test-area1";

            NodeFilter nodeFilter = new NodeFilter();

            nodeFilter.BaseURL = "http://www.frontera.info";
            nodeFilter.URL = "http://www.frontera.info/rosarito";



            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            List<HtmlNode> ContainerNodes = getContainerNodes(doc, tagContainer, attributeContainer, attributeContainerValue);
            List<Note> Notes = handlerContainer(ContainerNodes, tagContainer, attributeContainer, attributeContainerValue);
            return "hola";

        }


        public List<Note> handlerContainer(List<HtmlNode> nodex, string tagContainer, string atributeContainer, string atributeContainerValue)
        {
            List<Note> Notes = new List<Note>();
            tagContainer = "div";
            atributeContainer = "class";
            atributeContainerValue = "nota-ter";
            List<HtmlNode> NotesNodes = getNodesData(nodex[0], tagContainer, atributeContainer, atributeContainerValue);

            foreach (HtmlNode node in NotesNodes)
            {
                Note note = new Note();
                note = getNote(node);
                string url = "http://www.frontera.info" + note.link;
                note.link = url;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                tagContainer = "div";
                atributeContainer = "class";
                atributeContainerValue = "notecontent";
                List<HtmlNode> ContainerNodes = getContainerNodes(doc, tagContainer, atributeContainer, atributeContainerValue);
                note.Sample = ContainerNodes[0].InnerText;
                Notes.Add(note);
            }
            return Notes;
        }


        public List<HtmlNode> getContainerNodes(HtmlDocument doc, string tagContainer, string atributeContainer, string atributeContainerValue)
        {
            var NotesListNodes = doc.DocumentNode.Descendants(tagContainer)
                .Where(node => node.GetAttributeValue(atributeContainer, "")
                .Equals(atributeContainerValue)).ToList();

            return NotesListNodes;
        }

        public List<HtmlNode> getNodesData(HtmlNode nodex, string tagContainer, string attributeContainer, string attributeContainerValue)
        {
            var NotesNodes = nodex.Descendants(tagContainer)
                .Where(node => node.GetAttributeValue(attributeContainer, "")
                .Contains(attributeContainerValue)).ToList();

            return NotesNodes;
        }


        public Note getNote(HtmlNode node)
        {
            Note note = new Note();
            note.link = node.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
            note.Title = node.Descendants("img").FirstOrDefault().GetAttributeValue("alt", "");


            return note;
        }


    }
}