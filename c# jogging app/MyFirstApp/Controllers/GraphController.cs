using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.data;
using MyFirstApp.Models.Entities;

namespace MyFirstApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphController : Controller
    {
        private readonly GraphDbContext graphDbContext;

        public GraphController(GraphDbContext graphDbContext)
        {
            this.graphDbContext = graphDbContext;
        }

        #region Nodes
        [HttpGet("Nodes")]
        public async Task<IActionResult> GetAllNodes()
        {
            // Get Nodes form the database
            return Ok(await graphDbContext.Nodes.ToListAsync());
        }

        [HttpGet]
        [Route("Nodes/{id:Guid}")]
        [ActionName("GetNodeById")]
        public async Task<IActionResult> GetNodeById([FromRoute] Guid id)
        {
            var node = await graphDbContext.Nodes.FindAsync(id);
            // or
            //var node = await graphDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == id);

            if(node == null)
            {
                return NotFound();
            }
            return Ok(node);
        }

        [HttpPost("Nodes")]
        public async Task<IActionResult> AddNode(Node node) 
        { 
            node.Id = Guid.NewGuid();
            await graphDbContext.Nodes.AddAsync(node);
            await graphDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNodeById), new { id = node.Id }, node);
        }

        [HttpPut]
        [Route("Nodes/{id:Guid}")]
        public async Task<IActionResult> UpdateNode([FromRoute] Guid id, [FromBody] Node updateNode)
        {
            var existingNode = await graphDbContext.Nodes.FindAsync(id);
            if(existingNode == null)
            {
                return NotFound();
            }

            existingNode.Lat = updateNode.Lat;
            existingNode.Lon = updateNode.Lon;

            await graphDbContext.SaveChangesAsync();

            return Ok(existingNode);
        }

        [HttpDelete]
        [Route("Nodes/{id:Guid}")]
        public async Task<IActionResult> DeleteNode([FromRoute] Guid id)
        {
            var existingNode = await graphDbContext.Nodes.FindAsync(id);
            if (existingNode == null)
            {
                return NotFound();
            }
            
            graphDbContext.Nodes.Remove(existingNode);
            await graphDbContext.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region Edges
        [HttpGet("Edges")]
        public async Task<IActionResult> GetAllEdges()
        {
            // Get Nodes form the database

            return Ok(await graphDbContext.Edges.ToListAsync());

        }

        [HttpGet]
        [Route("Edges/{id:Guid}")]
        public async Task<IActionResult> GetEdgeById([FromRoute] Guid id)
        {
            var edge = await graphDbContext.Edges.FindAsync(id);
            // or
            //var node = await graphDbContext.Edges.FirstOrDefaultAsync(x => x.Id == id);

            if (edge == null)
            {
                return NotFound();
            }
            return Ok(edge);
        }


        [HttpPost("Edges")]
        public async Task<IActionResult> AddEdge(Edge edge)
        {
            edge.Id = Guid.NewGuid();
            await graphDbContext.Edges.AddAsync(edge);
            await graphDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEdgeById), new { id = edge.Id }, edge);
        }

        [HttpPut]
        [Route("Edges/{id:Guid}")]
        public async Task<IActionResult> UpdateEdge([FromRoute] Guid id, [FromBody] Edge updateEdge)
        {
            var existingEdge = await graphDbContext.Edges.FindAsync(id);
            if (existingEdge == null)
            {
                return NotFound();
            }

            existingEdge.StartNode = updateEdge.StartNode;
            existingEdge.StartNodeId = updateEdge.StartNodeId;
            existingEdge.EndNode = updateEdge.EndNode;
            existingEdge.EndNodeId = updateEdge.EndNodeId;
            existingEdge.IntermediateNodes = updateEdge.IntermediateNodes;
            existingEdge.EdgeCost = updateEdge.EdgeCost;
            existingEdge.Tag = updateEdge.Tag;

            await graphDbContext.SaveChangesAsync();

            return Ok(existingEdge);
        }

        [HttpDelete]
        [Route("Edges/{id:Guid}")]
        public async Task<IActionResult> DeleteEdge([FromRoute] Guid id)
        {
            var existingEdge = await graphDbContext.Edges.FindAsync(id);
            if (existingEdge == null)
            {
                return NotFound();
            }

            graphDbContext.Edges.Remove(existingEdge);
            await graphDbContext.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
